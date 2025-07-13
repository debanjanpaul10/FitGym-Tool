-- Script to insert FeesStatus and FeesPaymentHistory for 4 test users
-- Assumptions: MemberId 1-4, GUIDs as provided, all users joined Jan 1st of current year
-- CreatedBy/ModifiedBy: 'debanjanpaul10@gmail.com'
-- PaymentStatus: 1=Paid, 2=Due, 3=Overdue
-- FeesDurationId: 1=Monthly, 2=Quarterly, 3=Half-Yearly
-- FeesAmount: 1300 (Monthly), 3500 (Quarterly), 5500 (Half-Yearly)

DECLARE @User1GUID UNIQUEIDENTIFIER = 'C76E89BA-D4ED-4F7C-B48E-4618630F00CC',
        @User2GUID UNIQUEIDENTIFIER = '62F50506-125F-44DA-A2B0-01B3CFBE7F6E',
        @User3GUID UNIQUEIDENTIFIER = '5E80A6B2-2484-44DA-9B01-844C2AAC3FCA',
        @User4GUID UNIQUEIDENTIFIER = '46291EBE-9BEF-4F9F-9448-38F8E76F153B';

DECLARE @CurrentYear INT = YEAR(GETDATE());
DECLARE @CreatedBy NVARCHAR(MAX) = 'debanjanpaul10@gmail.com';

-- User 1: Monthly, paid Jan-Jun
INSERT INTO dbo.FeesStatus (MemberGuid, MemberId, FeesAmountDue, DueDate, LastPaymentDate, IsActive, FromDate, ToDate, PaymentStatus, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES (@User1GUID, 1, 0, DATEFROMPARTS(@CurrentYear, 7, 1), DATEFROMPARTS(@CurrentYear, 6, 1), 1, DATEFROMPARTS(@CurrentYear, 1, 1), DATEFROMPARTS(@CurrentYear, 6, 30), 1, 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);

-- Payment history for User 1
INSERT INTO dbo.FeesPaymentHistory (MemberGuid, MemberId, Amount, PaymentStatus, IsActive, FromDate, ToDate, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES
(@User1GUID, 1, 1300.00, 1, 1, DATEFROMPARTS(@CurrentYear, 1, 1), DATEFROMPARTS(@CurrentYear, 1, 31), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User1GUID, 1, 1300.00, 1, 1, DATEFROMPARTS(@CurrentYear, 2, 1), DATEFROMPARTS(@CurrentYear, 2, 28), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User1GUID, 1, 1300.00, 1, 1, DATEFROMPARTS(@CurrentYear, 3, 1), DATEFROMPARTS(@CurrentYear, 3, 31), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User1GUID, 1, 1300.00, 1, 1, DATEFROMPARTS(@CurrentYear, 4, 1), DATEFROMPARTS(@CurrentYear, 4, 30), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User1GUID, 1, 1300.00, 1, 1, DATEFROMPARTS(@CurrentYear, 5, 1), DATEFROMPARTS(@CurrentYear, 5, 31), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User1GUID, 1, 1300.00, 1, 1, DATEFROMPARTS(@CurrentYear, 6, 1), DATEFROMPARTS(@CurrentYear, 6, 30), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);

-- User 2: Quarterly, paid Jan-Mar and Apr-Jun
INSERT INTO dbo.FeesStatus (MemberGuid, MemberId, FeesAmountDue, DueDate, LastPaymentDate, IsActive, FromDate, ToDate, PaymentStatus, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES (@User2GUID, 2, 0, DATEFROMPARTS(@CurrentYear, 7, 1), DATEFROMPARTS(@CurrentYear, 6, 30), 1, DATEFROMPARTS(@CurrentYear, 1, 1), DATEFROMPARTS(@CurrentYear, 6, 30), 1, 2, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);

INSERT INTO dbo.FeesPaymentHistory (MemberGuid, MemberId, Amount, PaymentStatus, IsActive, FromDate, ToDate, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES
(@User2GUID, 2, 3500.00, 1, 1, DATEFROMPARTS(@CurrentYear, 1, 1), DATEFROMPARTS(@CurrentYear, 3, 31), 2, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User2GUID, 2, 3500.00, 1, 1, DATEFROMPARTS(@CurrentYear, 4, 1), DATEFROMPARTS(@CurrentYear, 6, 30), 2, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);

-- User 3: Half-Yearly, paid Jan-Jun
INSERT INTO dbo.FeesStatus (MemberGuid, MemberId, FeesAmountDue, DueDate, LastPaymentDate, IsActive, FromDate, ToDate, PaymentStatus, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES (@User3GUID, 3, 0, DATEFROMPARTS(@CurrentYear, 7, 1), DATEFROMPARTS(@CurrentYear, 6, 30), 1, DATEFROMPARTS(@CurrentYear, 1, 1), DATEFROMPARTS(@CurrentYear, 6, 30), 1, 3, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);

INSERT INTO dbo.FeesPaymentHistory (MemberGuid, MemberId, Amount, PaymentStatus, IsActive, FromDate, ToDate, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES
(@User3GUID, 3, 5500.00, 1, 1, DATEFROMPARTS(@CurrentYear, 1, 1), DATEFROMPARTS(@CurrentYear, 6, 30), 3, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);

-- User 4: Monthly, not paid Apr-Jun
INSERT INTO dbo.FeesStatus (MemberGuid, MemberId, FeesAmountDue, DueDate, LastPaymentDate, IsActive, FromDate, ToDate, PaymentStatus, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES (@User4GUID, 4, 3900.00, DATEFROMPARTS(@CurrentYear, 7, 1), DATEFROMPARTS(@CurrentYear, 3, 31), 1, DATEFROMPARTS(@CurrentYear, 1, 1), DATEFROMPARTS(@CurrentYear, 6, 30), 3, 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);

INSERT INTO dbo.FeesPaymentHistory (MemberGuid, MemberId, Amount, PaymentStatus, IsActive, FromDate, ToDate, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES
(@User4GUID, 4, 1300.00, 1, 1, DATEFROMPARTS(@CurrentYear, 1, 1), DATEFROMPARTS(@CurrentYear, 1, 31), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User4GUID, 4, 1300.00, 1, 1, DATEFROMPARTS(@CurrentYear, 2, 1), DATEFROMPARTS(@CurrentYear, 2, 28), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User4GUID, 4, 1300.00, 1, 1, DATEFROMPARTS(@CurrentYear, 3, 1), DATEFROMPARTS(@CurrentYear, 3, 31), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User4GUID, 4, 1300.00, 3, 1, DATEFROMPARTS(@CurrentYear, 4, 1), DATEFROMPARTS(@CurrentYear, 4, 30), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User4GUID, 4, 1300.00, 3, 1, DATEFROMPARTS(@CurrentYear, 5, 1), DATEFROMPARTS(@CurrentYear, 5, 31), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User4GUID, 4, 1300.00, 3, 1, DATEFROMPARTS(@CurrentYear, 6, 1), DATEFROMPARTS(@CurrentYear, 6, 30), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);
