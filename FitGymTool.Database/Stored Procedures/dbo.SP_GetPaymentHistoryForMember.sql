/*
	Author: Debanjan Paul
	Purpose: Gets the payment history data for member.
	DateCreated: 23-July-2025
*/

CREATE PROCEDURE [dbo].[SP_GetPaymentHistoryForMember]
(
	@MemberEmailId NVARCHAR(MAX) = ''
)
AS
BEGIN
SET NOCOUNT ON;
BEGIN TRY
	IF (@MemberEmailId = '')
	BEGIN
		SELECT 
			MD.MemberId, MD.MemberName, MD.MemberEmail, MSM.StatusName AS MemberStatus, FPH.Amount, FPH.FromDate, FPH.ToDate, FPSM.StatusName AS FeesPaymentStatus
		FROM dbo.FeesPaymentHistory FPH (NOLOCK)
			INNER JOIN dbo.MemberDetails MD (NOLOCK) ON MD.MemberId=FPH.MemberId AND FPH.MemberGuid=MD.MemberGuid AND MD.IsActive=1
			INNER JOIN dbo.MembershipStatusMapping MSM (NOLOCK) ON MSM.Id=MD.MembershipStatusId AND MSM.IsActive=1
			INNER JOIN dbo.FeesPaymentStatusMapping FPSM (NOLOCK) ON FPSM.Id=FPH.PaymentStatusId AND FPSM.IsActive=1
		WHERE FPH.IsActive=1
	END
	ELSE
	BEGIN
		SELECT 
			MD.MemberId, MD.MemberName, MD.MemberEmail, MSM.StatusName AS MemberStatus, FPH.Amount, FPH.FromDate, FPH.ToDate, FPSM.StatusName AS FeesPaymentStatus
		FROM dbo.FeesPaymentHistory FPH (NOLOCK)
			INNER JOIN dbo.MemberDetails MD (NOLOCK) ON MD.MemberId=FPH.MemberId AND FPH.MemberGuid=MD.MemberGuid AND MD.IsActive=1
			INNER JOIN dbo.MembershipStatusMapping MSM (NOLOCK) ON MSM.Id=MD.MembershipStatusId AND MSM.IsActive=1
			INNER JOIN dbo.FeesPaymentStatusMapping FPSM (NOLOCK) ON FPSM.Id=FPH.PaymentStatusId AND FPSM.IsActive=1
		WHERE FPH.IsActive=1 AND MD.MemberEmail=@MemberEmailId
	END

END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE(),
	@ErrorProcedure NVARCHAR(MAX) = ERROR_PROCEDURE(),
	@ErrorLine NVARCHAR(MAX) = CAST(ERROR_LINE() AS NVARCHAR(MAX))

	EXEC [dbo].[SP_InsertErrorLog] @ErrorMessage, @ErrorProcedure, @ErrorLine
END CATCH

END