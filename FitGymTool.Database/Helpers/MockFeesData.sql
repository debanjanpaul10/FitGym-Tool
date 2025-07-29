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

DECLARE @User1JoinDate DATE, @User2JoinDate DATE, @User3JoinDate DATE, @User4JoinDate DATE;
SELECT @User1JoinDate = MemberJoinDate FROM dbo.MemberDetails WHERE MemberId = 1;
SELECT @User2JoinDate = MemberJoinDate FROM dbo.MemberDetails WHERE MemberId = 2;
SELECT @User3JoinDate = MemberJoinDate FROM dbo.MemberDetails WHERE MemberId = 3;
SELECT @User4JoinDate = MemberJoinDate FROM dbo.MemberDetails WHERE MemberId = 4;

DECLARE @CreatedBy NVARCHAR(MAX) = 'debanjanpaul10@gmail.com';

-- User 1: Monthly, paid 6 periods from joining date
INSERT INTO dbo.FeesStatus (MemberGuid, MemberId, FeesAmountDue, DueDate, LastPaymentDate, IsActive, FromDate, ToDate, PaymentStatusId, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES (@User1GUID, 1, 0,
    DATEADD(MONTH, 6, EOMONTH(@User1JoinDate)),
    EOMONTH(DATEADD(MONTH, 5, @User1JoinDate)),
    1,
    @User1JoinDate,
    EOMONTH(DATEADD(MONTH, 5, @User1JoinDate)),
    1, 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);

-- Payment history for User 1
INSERT INTO dbo.FeesPaymentHistory (MemberGuid, MemberId, Amount, PaymentStatusId, IsActive, FromDate, ToDate, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES
(@User1GUID, 1, 1300.00, 1, 1, @User1JoinDate, EOMONTH(@User1JoinDate), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User1GUID, 1, 1300.00, 1, 1, DATEADD(MONTH, 1, @User1JoinDate), EOMONTH(DATEADD(MONTH, 1, @User1JoinDate)), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User1GUID, 1, 1300.00, 1, 1, DATEADD(MONTH, 2, @User1JoinDate), EOMONTH(DATEADD(MONTH, 2, @User1JoinDate)), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User1GUID, 1, 1300.00, 1, 1, DATEADD(MONTH, 3, @User1JoinDate), EOMONTH(DATEADD(MONTH, 3, @User1JoinDate)), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User1GUID, 1, 1300.00, 1, 1, DATEADD(MONTH, 4, @User1JoinDate), EOMONTH(DATEADD(MONTH, 4, @User1JoinDate)), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User1GUID, 1, 1300.00, 1, 1, DATEADD(MONTH, 5, @User1JoinDate), EOMONTH(DATEADD(MONTH, 5, @User1JoinDate)), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);

-- User 2: Quarterly, paid 2 periods from joining date
INSERT INTO dbo.FeesStatus (MemberGuid, MemberId, FeesAmountDue, DueDate, LastPaymentDate, IsActive, FromDate, ToDate, PaymentStatusId, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES (@User2GUID, 2, 0,
    DATEADD(MONTH, 6, EOMONTH(@User2JoinDate)),
    EOMONTH(DATEADD(MONTH, 5, @User2JoinDate)),
    1,
    @User2JoinDate,
    EOMONTH(DATEADD(MONTH, 5, @User2JoinDate)),
    1, 2, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);

INSERT INTO dbo.FeesPaymentHistory (MemberGuid, MemberId, Amount, PaymentStatusId, IsActive, FromDate, ToDate, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES
(@User2GUID, 2, 3500.00, 1, 1, @User2JoinDate, EOMONTH(DATEADD(MONTH, 2, @User2JoinDate)), 2, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User2GUID, 2, 3500.00, 1, 1, DATEADD(MONTH, 3, @User2JoinDate), EOMONTH(DATEADD(MONTH, 5, @User2JoinDate)), 2, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);

-- User 3: Half-Yearly, paid 1 period from joining date
INSERT INTO dbo.FeesStatus (MemberGuid, MemberId, FeesAmountDue, DueDate, LastPaymentDate, IsActive, FromDate, ToDate, PaymentStatusId, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES (@User3GUID, 3, 0,
    DATEADD(MONTH, 6, EOMONTH(@User3JoinDate)),
    EOMONTH(DATEADD(MONTH, 5, @User3JoinDate)),
    1,
    @User3JoinDate,
    EOMONTH(DATEADD(MONTH, 5, @User3JoinDate)),
    1, 3, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);

INSERT INTO dbo.FeesPaymentHistory (MemberGuid, MemberId, Amount, PaymentStatusId, IsActive, FromDate, ToDate, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES
(@User3GUID, 3, 5500.00, 1, 1, @User3JoinDate, EOMONTH(DATEADD(MONTH, 5, @User3JoinDate)), 3, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);

-- User 4: Monthly, not paid Apr-Jun (last 3 periods overdue)
INSERT INTO dbo.FeesStatus (MemberGuid, MemberId, FeesAmountDue, DueDate, LastPaymentDate, IsActive, FromDate, ToDate, PaymentStatusId, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES (@User4GUID, 4, 3900.00,
    DATEADD(MONTH, 6, EOMONTH(@User4JoinDate)),
    EOMONTH(DATEADD(MONTH, 2, @User4JoinDate)),
    1,
    @User4JoinDate,
    EOMONTH(DATEADD(MONTH, 5, @User4JoinDate)),
    3, 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);

INSERT INTO dbo.FeesPaymentHistory (MemberGuid, MemberId, Amount, PaymentStatusId, IsActive, FromDate, ToDate, FeesDurationId, DateCreated, CreatedBy, DateModified, ModifiedBy)
VALUES
(@User4GUID, 4, 1300.00, 1, 1, @User4JoinDate, EOMONTH(@User4JoinDate), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User4GUID, 4, 1300.00, 1, 1, DATEADD(MONTH, 1, @User4JoinDate), EOMONTH(DATEADD(MONTH, 1, @User4JoinDate)), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User4GUID, 4, 1300.00, 1, 1, DATEADD(MONTH, 2, @User4JoinDate), EOMONTH(DATEADD(MONTH, 2, @User4JoinDate)), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User4GUID, 4, 1300.00, 3, 1, DATEADD(MONTH, 3, @User4JoinDate), EOMONTH(DATEADD(MONTH, 3, @User4JoinDate)), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User4GUID, 4, 1300.00, 3, 1, DATEADD(MONTH, 4, @User4JoinDate), EOMONTH(DATEADD(MONTH, 4, @User4JoinDate)), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy),
(@User4GUID, 4, 1300.00, 3, 1, DATEADD(MONTH, 5, @User4JoinDate), EOMONTH(DATEADD(MONTH, 5, @User4JoinDate)), 1, GETUTCDATE(), @CreatedBy, GETUTCDATE(), @CreatedBy);
