CREATE PROCEDURE [dbo].[GetCurrentFeesPaymentStatus]
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT 
		MD.MemberId, MD.MemberName, MSM.StatusName, FS.FeesAmountDue, FS.DueDate, FS.LastPaymentDate, FPSM.StatusName
	FROM FeesStatus FS (NOLOCK) 
		INNER JOIN dbo.MemberDetails MD (NOLOCK) ON MD.MemberId=FS.MemberId AND FS.MemberGuid=MD.MemberGuid AND MD.IsActive=1
		INNER JOIN dbo.MembershipStatusMapping MSM (NOLOCK) ON MSM.Id=MD.MembershipStatus AND MSM.IsActive=1
		INNER JOIN dbo.FeesPaymentStatusMapping FPSM (NOLOCK) ON FPSM.Id=FS.PaymentStatus AND FPSM.IsActive=1
	WHERE FS.IsActive=1

END
GO