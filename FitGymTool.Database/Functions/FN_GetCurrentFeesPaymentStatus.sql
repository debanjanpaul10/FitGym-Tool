/*
    Author: Debanjan Paul
    Purpose: Returns the current fees payment status for all active members.
    DateCreated: 13-July-2025
*/
CREATE FUNCTION [dbo].[fn_GetCurrentFeesPaymentStatus]()
RETURNS TABLE
AS
RETURN
(
    SELECT 
        MD.MemberId, MD.MemberName, MSM.StatusName AS MemberStatus, FS.FeesAmountDue, FS.DueDate, FS.LastPaymentDate, FPSM.StatusName AS FeesPaymentStatus
    FROM FeesStatus FS (NOLOCK) 
        INNER JOIN dbo.MemberDetails MD (NOLOCK) ON MD.MemberId=FS.MemberId AND FS.MemberGuid=MD.MemberGuid AND MD.IsActive=1
        INNER JOIN dbo.MembershipStatusMapping MSM (NOLOCK) ON MSM.Id=MD.MembershipStatus AND MSM.IsActive=1
        INNER JOIN dbo.FeesPaymentStatusMapping FPSM (NOLOCK) ON FPSM.Id=FS.PaymentStatus AND FPSM.IsActive=1
    WHERE FS.IsActive=1
)