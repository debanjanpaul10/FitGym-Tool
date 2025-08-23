CREATE TABLE [dbo].[AIChatbotPrompts]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[Area] NVARCHAR(100) NOT NULL,
	[PromptName] NVARCHAR(MAX) NOT NULL,
	[PromptDescription] NVARCHAR(MAX) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT 1,
	[DateCreated] DATE NOT NULL DEFAULT GETDATE(),
	[CreatedBy] NVARCHAR(MAX) NOT NULL,
	[DateModified] DATE NOT NULL DEFAULT GETDATE(),
	[ModifiedBy] NVARCHAR(MAX) NOT NULL,
)
GO;

CREATE NONCLUSTERED INDEX ix_AIChatbotPrompts_Id_IsActive ON [dbo].[AIChatbotPrompts]([Id], [IsActive])
GO;