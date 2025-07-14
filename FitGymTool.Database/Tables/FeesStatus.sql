CREATE TABLE [dbo].[FeesStatus]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[MemberGuid] UNIQUEIDENTIFIER NOT NULL,
	[MemberId] INT NOT NULL,
	[FeesAmountDue] DECIMAL(18, 2) NULL,
	[DueDate] DATE NOT NULL,
	[LastPaymentDate] DATE NULL,
	[IsActive] BIT NOT NULL DEFAULT(1),
	[FromDate] DATETIME NOT NULL,
	[ToDate] DATETIME NOT NULL,
	[PaymentStatusId] INT NOT NULL,
	[FeesDurationId] INT NOT NULL,
	[DateCreated] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] NVARCHAR(MAX) NOT NULL,
	[DateModified] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT [FK_FeesStatus_MemberDetails] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[MemberDetails]([MemberId]),
	CONSTRAINT [FK_FeesStatus_FeesPaymentStatusMapping] FOREIGN KEY ([PaymentStatusId]) REFERENCES [dbo].[FeesPaymentStatusMapping]([Id]),
	CONSTRAINT [FK_FeesStatus_FeesDurationMapping] FOREIGN KEY ([FeesDurationId]) REFERENCES [dbo].[FeesDurationMapping]([Id])
)
GO;

CREATE NONCLUSTERED INDEX IX_FeesStatus_MemberGuid_MemberId_PaymentStatus_IsActive ON [dbo].[FeesStatus] ([MemberGuid], [MemberId], [PaymentStatusId], [IsActive])
GO;
