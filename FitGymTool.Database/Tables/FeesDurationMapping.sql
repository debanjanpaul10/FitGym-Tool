CREATE TABLE [dbo].[FeesDurationMapping]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[DurationTypeName] NVARCHAR(50) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1,
	[DateCreated] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] NVARCHAR(MAX) NOT NULL,
	[DateModified] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] NVARCHAR(MAX) NOT NULL
)
GO;

CREATE NONCLUSTERED INDEX ix_FeesDurationMapping_Id_IsActive ON [dbo].[FeesDurationMapping] ([Id], [IsActive])
GO;