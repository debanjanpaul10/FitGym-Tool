/*
    Author: Debanjan Paul
    Purpose: Returns the payment history data for all the members.
    DateCreated: 13-July-2025
*/

CREATE FUNCTION [dbo].[fn_GetPaymentHistoryDataForMembers]()
RETURNS TABLE
AS
RETURN
(
    SELECT 
        MD.MemberId, MD.MemberName, MD.MemberEmail, MSM.StatusName AS MemberStatus, FPH.Amount, FPH.FromDate, FPH.ToDate, FPSM.StatusName AS FeesPaymentStatus
    FROM dbo.FeesPaymentHistory FPH (NOLOCK)
        INNER JOIN dbo.MemberDetails MD (NOLOCK) ON MD.MemberId=FPH.MemberId AND FPH.MemberGuid=MD.MemberGuid AND MD.IsActive=1
        INNER JOIN dbo.MembershipStatusMapping MSM (NOLOCK) ON MSM.Id=MD.MembershipStatusId AND MSM.IsActive=1
        INNER JOIN dbo.FeesPaymentStatusMapping FPSM (NOLOCK) ON FPSM.Id=FPH.PaymentStatusId AND FPSM.IsActive=1
    WHERE FPH.IsActive=1
)