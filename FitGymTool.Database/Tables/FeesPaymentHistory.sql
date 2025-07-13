CREATE TABLE [dbo].[FeesPaymentHistory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[MemberGuid] UNIQUEIDENTIFIER NOT NULL,
	[MemberId] INT NOT NULL,
	[Amount] DECIMAL (10, 2) NULL,
	[PaymentStatus] INT NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1,
	[FromDate] DATETIME NOT NULL,
	[ToDate] DATETIME NOT NULL,
	[FeesDurationId] INT NOT NULL,
	[DateCreated] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] NVARCHAR(MAX) NOT NULL,
	[DateModified] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT [FK_FeesPaymentHistory_MemberDetails_MI] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[MemberDetails]([MemberId]),
	CONSTRAINT [FK_FeesPaymentHistory_FeesPaymentStatusMapping] FOREIGN KEY ([PaymentStatus]) REFERENCES [dbo].[FeesPaymentStatusMapping]([Id]),
	CONSTRAINT [FK_FeesPaymentHistory_FeesDurationMapping] FOREIGN KEY ([FeesDurationId]) REFERENCES [dbo].[FeesDurationMapping]([Id])
)
GO;

CREATE NONCLUSTERED INDEX ix_FeesPaymentHistory_MemberGuid_MemberId ON [dbo].[FeesPaymentHistory] ([MemberGuid], [MemberId])
GO;

CREATE NONCLUSTERED INDEX ix_FeesPaymentHistory_Amount_MemberId_IsActive ON [dbo].[FeesPaymentHistory] ([MemberId], [Amount], [IsActive])
GO;