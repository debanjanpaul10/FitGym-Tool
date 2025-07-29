/*
    Author: Debanjan Paul
    Purpose: Returns the current fees payment status for all active members.
    DateCreated: 13-July-2025
*/
CREATE FUNCTION [dbo].[FN_GetCurrentMembersFeesStatus]()
RETURNS TABLE
AS
RETURN
(
   SELECT 
    MD.MemberId AS [MemberId], 
    MD.MemberName AS [MemberName], 
    MD.MemberEmail AS [MemberEmail], 
    MSM.StatusName AS MemberStatus, 
    CASE WHEN FPSM.StatusName <> 'Paid' THEN FPH.Amount ELSE 0.00 END AS [DueAmount], 
    CASE WHEN FPSM.StatusName <> 'Paid' THEN FPH.FromDate ELSE NULL END AS [DueDate],
    CASE WHEN FPSM.StatusName = 'Paid' THEN FPH.Amount ELSE 0.00 END AS [PaidAmount],
    FPSM.StatusName AS [FeesPaymentStatus]
FROM (
    SELECT *,
           ROW_NUMBER() OVER (PARTITION BY MemberId ORDER BY ToDate DESC) as rn
    FROM FeesPaymentHistory (NOLOCK)
    WHERE IsActive = 1
) FPH
INNER JOIN dbo.MemberDetails MD (NOLOCK) 
    ON MD.MemberId = FPH.MemberId 
    AND FPH.MemberGuid = MD.MemberGuid 
    AND MD.IsActive = 1
INNER JOIN dbo.MembershipStatusMapping MSM (NOLOCK) 
    ON MSM.Id = MD.MembershipStatusId 
    AND MSM.IsActive = 1
INNER JOIN dbo.FeesPaymentStatusMapping FPSM (NOLOCK) 
    ON FPSM.Id = FPH.PaymentStatusId 
    AND FPSM.IsActive = 1
WHERE FPH.rn = 1
)