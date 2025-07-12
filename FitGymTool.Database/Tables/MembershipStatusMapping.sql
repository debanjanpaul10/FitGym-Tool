CREATE TABLE [dbo].[MembershipStatusMapping]
(
	[Id] INT NOT NULL IDENTITY(1, 1),
	[StatusName] NVARCHAR(50) NOT NULL,
	[StatusId] INT NOT NULL PRIMARY KEY,
	[IsActive] BIT NOT NULL DEFAULT 1,
	[DateCreated] DATETIME NOT NULL DEFAULT GETUTCDATE()
)
GO;
CREATE NONCLUSTERED INDEX IX_MembershipStatusMapping_StatusId_IsActive ON [dbo].[MembershipStatusMapping] ([StatusId], [IsActive])
GO;
