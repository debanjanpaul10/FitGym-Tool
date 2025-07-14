CREATE TABLE [dbo].[FeesStructure]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	[FeesDurationId] INT NOT NULL,
	[FeesAmount] DECIMAL(10, 2) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1,
	[DateCreated] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] NVARCHAR(MAX) NOT NULL,
	[DateModified] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT FK_FeesStructure_FeesDurationMapping FOREIGN KEY ([FeesDurationId]) REFERENCES [dbo].[FeesDurationMapping]([Id])
)
GO;

CREATE NONCLUSTERED INDEX ix_FeesStructure_Id_FeesDurationId_IsActive ON [dbo].[FeesStructure]([Id], [FeesDurationId], [IsActive])
GO;
