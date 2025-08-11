-------------------------------------------------------------------------------------------------------------------------------------------------
--	|		Author			|		Modification Date		|					                Description						                |
-------------------------------------------------------------------------------------------------------------------------------------------------
--	|		Debanjan Paul	|		10-08-2025				|	            Updates the member's membership fees status 	                    |
-------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[SP_UpdateMemberFeesData]
(
    @UpdateMemberFeesData [dbo].[UpdateMemberFeesTableType] READONLY
)
AS
BEGIN
BEGIN TRY
    BEGIN TRANSACTION
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM @UpdateMemberFeesData)
    BEGIN
        RAISERROR('No member data provided', 16, 1);
        RETURN;
    END
    
    DECLARE @TotalUpdateCount INT = 0;
    DECLARE @PaidStatusId INT;
    DECLARE @CurrentMemberEmail NVARCHAR(MAX) = '';
    SET @CurrentMemberEmail = (SELECT MemberEmail FROM @UpdateMemberFeesData);
    
    -- Get the PaidStatusId once for all operations
    SELECT @PaidStatusId = fps.Id
    FROM [dbo].[FeesPaymentStatusMapping] fps (NOLOCK)
    WHERE fps.[IsActive] = 1 AND fps.[StatusName] = 'Paid';
    
    -- Validate PaidStatusId was found
    IF @PaidStatusId IS NULL
    BEGIN
        RAISERROR ('Paid status mapping not found', 16, 1);
        RETURN;
    END
        
    UPDATE fph
    SET [PaymentStatusId] = @PaidStatusId, 
        [DateModified] = GETDATE(), 
        [ModifiedBy] = umfd.ModifiedBy
    FROM [dbo].[FeesPaymentHistory] fph WITH (NOLOCK)
        INNER JOIN [dbo].[MemberDetails] md (NOLOCK) ON fph.MemberId = md.MemberId
        INNER JOIN @UpdateMemberFeesData umfd ON md.MemberEmail = umfd.MemberEmail AND fph.FromDate = umfd.FromDate AND fph.ToDate = umfd.ToDate AND fph.Amount = umfd.Amount
    WHERE md.[IsActive] = 1 AND fph.[PaymentStatusId] = 2 AND fph.[IsActive] = 1;
        
    EXEC [dbo].[SP_CalculateNextPaymentDataForMember] @CurrentMemberEmail, 0;

    IF @@TRANCOUNT > 0
        COMMIT TRANSACTION;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE
            @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE(),
            @ErrorProcedure NVARCHAR(MAX) = ISNULL(ERROR_PROCEDURE(), 'SP_UpdateMemberFeesData'),
            @ErrorLine INT = ERROR_LINE(),
            @ErrorSeverity INT = ERROR_SEVERITY(),
            @ErrorState INT = ERROR_STATE();

        EXEC [dbo].[SP_InsertErrorLog] @ErrorMessage, @ErrorProcedure, @ErrorLine;
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END