CREATE TABLE [dbo].[AIFeatures]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	[ServiceName] NVARCHAR(MAX) NOT NULL,
	[ServiceDescription] NVARCHAR(MAX) NOT NULL,
	[ServiceStatusId] INT NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1,
	[DateCreated] DATE NOT NULL DEFAULT GETDATE(),
	[CreatedBy] NVARCHAR(MAX) NOT NULL,
	[DateModified] DATE NOT NULL DEFAULT GETDATE(),
	[ModifiedBy] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT [FK_AIFeatures_AIServiceStatusMapping] FOREIGN KEY ([ServiceStatusId]) REFERENCES [dbo].[AIServiceStatusMapping]([Id]),
)
GO;

CREATE NONCLUSTERED INDEX ix_AIFeatures_Id_ServiceStatusId_IsActive ON [dbo].[AIFeatures]([Id], [ServiceStatusId], [IsActive])
GO;
