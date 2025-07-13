CREATE TABLE [dbo].[FeesPaymentStatusMapping]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[StatusName] NVARCHAR(50) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1,
	[DateCreated] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] NVARCHAR(MAX) NULL
)
GO;

CREATE NONCLUSTERED INDEX IX_FeesPaymentStatusMapping_Id_IsActive_StatusName ON [dbo].[FeesPaymentStatusMapping] ([Id], [StatusName], [IsActive])
GO;

