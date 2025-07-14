/*
	Author: Debanjan Paul
	Purpose: Returns the current fees payment status for all active members.
	DateCreated: 13-July-2025
*/

CREATE FUNCTION [dbo].[FN_GetCurrentFeesAndRevenueStatus]()
RETURNS TABLE
AS
RETURN
(
	SELECT 
		MD.MemberId, MD.MemberEmail, MSM.StatusName AS MembershipStatus, FPH.Amount, FPH.FromDate, FPH.ToDate, FPSM.StatusName AS FeesStatus 
	FROM dbo.FeesPaymentHistory FPH (NOLOCK)
  	  INNER JOIN dbo.MemberDetails MD (NOLOCK) ON MD.MemberId=FPH.MemberId AND MD.MemberGuid=FPH.MemberGuid AND MD.IsActive=1 
  	  INNER JOIN dbo.MembershipStatusMapping MSM (NOLOCK) ON MSM.Id=MD.MembershipStatusId AND MSM.IsActive=1
  	  INNER JOIN dbo.FeesPaymentStatusMapping FPSM (NOLOCK) ON FPSM.Id=FPH.PaymentStatusId AND FPSM.IsActive=1
	WHERE FPH.[ToDate] BETWEEN EOMONTH(GETDATE(), -1) AND EOMONTH(GETDATE()) AND FPH.IsActive=1
)
