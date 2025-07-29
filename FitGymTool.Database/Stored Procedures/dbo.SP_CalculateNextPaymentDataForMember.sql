-------------------------------------------------------------------------------------------------------------------------------------------------
--	|		Author			|		Modification Date		|					Description														|
-------------------------------------------------------------------------------------------------------------------------------------------------
--	|		Debanjan Paul	|		26-07-2025				|	Calculates the next payment data for member										|
-------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[SP_CalculateNextPaymentDataForMember]
(
	@MemberEmailId NVARCHAR(MAX) = ''
)
AS
BEGIN
SET NOCOUNT ON;

BEGIN TRANSACTION;
BEGIN TRY

	DECLARE @DueStatusId INT;
	DECLARE @ProcedureName NVARCHAR(MAX) = 'SP_CalculateNextPaymentDataForMember';
	DECLARE @CurrentDate DATETIME = GETDATE();
	
	-- Get Due status ID once
	SELECT @DueStatusId = Id 
	FROM [dbo].[FeesPaymentStatusMapping] WITH (NOLOCK) 
	WHERE IsActive = 1 AND [StatusName] = 'Due';

	-- Create temp table to store members who need payment calculation
	CREATE TABLE #MembersToProcess (
		MemberId INT,
		MemberEmail NVARCHAR(MAX),
		MemberGuid UNIQUEIDENTIFIER,
		FeesDurationId INT,
		FeesAmount DECIMAL(18,2)
	);

	-- Insert members who need processing (no active payment period covering current date)
	INSERT INTO #MembersToProcess (MemberId, MemberEmail, MemberGuid, FeesDurationId, FeesAmount)
	SELECT DISTINCT 
		MD.MemberId,
		MD.MemberEmail,
		MD.MemberGuid,
		MFPD.FeesDurationId,
		FS.FeesAmount
	FROM dbo.MemberDetails MD WITH (NOLOCK)
		INNER JOIN dbo.MemberFeesPaymentDurationMapping MFPD WITH (NOLOCK) 
			ON MFPD.MemberId = MD.MemberId AND MFPD.IsActive = 1
		INNER JOIN dbo.FeesStructure FS WITH (NOLOCK) 
			ON FS.FeesDurationId = MFPD.FeesDurationId AND FS.IsActive = 1
	WHERE MD.IsActive = 1
		AND (@MemberEmailId IS NULL OR @MemberEmailId = '' OR MD.MemberEmail = @MemberEmailId)
		AND NOT EXISTS (
			SELECT 1 
			FROM [dbo].[FeesPaymentHistory] FPH WITH (NOLOCK)
			WHERE FPH.IsActive = 1 
				AND FPH.MemberId = MD.MemberId 
				AND @CurrentDate BETWEEN FPH.[FromDate] AND FPH.[ToDate]
		);

	-- Create temp table to store new records that will be inserted
	CREATE TABLE #NewRecords (
		MemberId INT,
		MemberGuid UNIQUEIDENTIFIER,
		Amount DECIMAL(18,2),
		FromDate DATETIME,
		ToDate DATETIME,
		FeesDurationId INT
	);

	-- Insert data for new records that will be created
	INSERT INTO #NewRecords (MemberId, MemberGuid, Amount, FromDate, ToDate, FeesDurationId)
	SELECT 
		MTP.MemberId,
		MTP.MemberGuid,
		MTP.FeesAmount,
		NPD.FromDate,
		NPD.ToDate,
		MTP.FeesDurationId
	FROM #MembersToProcess MTP
		CROSS APPLY [dbo].[FN_CalculateNextTimeframeFeesPayment](MTP.MemberEmail) NPD
	WHERE NOT EXISTS (
		SELECT 1 
		FROM [dbo].[FeesPaymentHistory] FPH WITH (NOLOCK)
		WHERE FPH.IsActive = 1 
			AND FPH.MemberId = MTP.MemberId 
			AND FPH.[FromDate] = NPD.FromDate 
			AND FPH.[ToDate] = NPD.ToDate
	);

	-- Update previous records with statusId 2 to statusId 3 for members who will get new records
	-- Only update if new records will have statusId 2 (Due status)
	IF @DueStatusId = 2
	BEGIN
		UPDATE FPH
		SET PaymentStatusId = 3,
			DateModified = GETUTCDATE(),
			ModifiedBy = @ProcedureName
		FROM [dbo].[FeesPaymentHistory] FPH
			INNER JOIN #NewRecords NR ON FPH.MemberId = NR.MemberId
		WHERE FPH.IsActive = 1 
			AND FPH.PaymentStatusId = 2;
	END

	-- Process each member and insert payment records
	INSERT INTO [dbo].[FeesPaymentHistory] (MemberGuid, MemberId, Amount, PaymentStatusId, IsActive, FromDate, ToDate, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
	SELECT 
		NR.MemberGuid,
		NR.MemberId,
		NR.Amount,
		@DueStatusId,
		1,
		NR.FromDate,
		NR.ToDate,
		NR.FeesDurationId,
		GETUTCDATE(),
		@ProcedureName,
		GETUTCDATE(),
		@ProcedureName
	FROM #NewRecords NR;

	-- Clean up temp tables
	DROP TABLE IF EXISTS #NewRecords;

	-- Clean up temp table
	DROP TABLE IF EXISTS #MembersToProcess;

	
	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;

END TRY

BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;

	DECLARE 
		@ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE(),
		@ErrorProcedure NVARCHAR(MAX) = ERROR_PROCEDURE(),
		@ErrorLine NVARCHAR(MAX) = CAST(ERROR_LINE() AS NVARCHAR(MAX))

	EXEC [dbo].[SP_InsertErrorLog] @ErrorMessage, @ErrorProcedure, @ErrorLine

END CATCH

END
