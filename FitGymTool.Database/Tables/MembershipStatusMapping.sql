CREATE TABLE [dbo].[MembershipStatusMapping]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[StatusName] NVARCHAR(50) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1,
	[DateCreated] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] NVARCHAR(MAX) NOT NULL,
	[DateModified] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] NVARCHAR(MAX) NOT NULL
)
GO;
CREATE NONCLUSTERED INDEX IX_MembershipStatusMapping_StatusId_IsActive ON [dbo].[MembershipStatusMapping] ([Id], [IsActive])
GO;
