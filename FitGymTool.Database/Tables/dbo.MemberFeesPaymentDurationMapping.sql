CREATE TABLE [dbo].[MemberFeesPaymentDurationMapping]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[MemberId] INT NOT NULL,
	[MemberEmail] NVARCHAR(MAX) NOT NULL,
	[FeesDurationId] INT NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1,
	[DateCreated] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] NVARCHAR(MAX) NOT NULL,
	[DateModified] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT [FK_MemberFeesPaymentDurationMapping_FeesDurationMapping] FOREIGN KEY ([FeesDurationId]) REFERENCES [dbo].[FeesPaymentStatusMapping]([Id]),
	CONSTRAINT [FK_MemberFeesPaymentDurationMapping_MemberDetails_MI] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[MemberDetails]([MemberId])
)
GO;

CREATE NONCLUSTERED INDEX ix_MemberFeesPaymentDurationMapping_MemberId_FeesDurationId_IsActive ON [dbo].[MemberFeesPaymentDurationMapping]([MemberId], [FeesDurationId], [IsActive])
GO;