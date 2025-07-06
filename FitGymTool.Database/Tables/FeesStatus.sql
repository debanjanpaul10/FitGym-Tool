CREATE TABLE [dbo].[FeesStatus]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[MemberGuid] UNIQUEIDENTIFIER NOT NULL,
	[MemberId] INT NOT NULL,
	[FeesAmountDue] DECIMAL(18, 2) NULL,
	[DueDate] DATE NOT NULL,
	[LastPaymentDate] DATE NULL,
	[IsActive] BIT NOT NULL DEFAULT(1),
	[PaymentStatus] INT NOT NULL DEFAULT(1),
	CONSTRAINT [FK_FeesStatus_MemberDetails] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[MemberDetails]([MemberId])
)
GO

CREATE NONCLUSTERED INDEX IX_FeesStatus_MemberGuid ON [dbo].[FeesStatus] ([MemberGuid])
GO;

CREATE NONCLUSTERED INDEX IX_FeesStatus_MemberId ON [dbo].[FeesStatus] ([MemberId])
GO;

CREATE NONCLUSTERED INDEX IX_FeesStatus_PaymentStatus ON [dbo].[FeesStatus] ([PaymentStatus])
GO;
