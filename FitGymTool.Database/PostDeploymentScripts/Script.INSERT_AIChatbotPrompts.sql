IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[AIChatbotPrompts] WHERE [IsActive]=1 AND [PromptName]='Give me the list of all members')
BEGIN
	INSERT INTO [dbo].[AIChatbotPrompts] (Area, PromptName, PromptDescription, IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES ('SQL', 'Give me the list of all members', 'Will get the list of all members currently in the system', 1, GETDATE(), 'System', GETDATE(), 'System')
END

IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[AIChatbotPrompts] WHERE [IsActive]=1 AND [PromptName]='Give me the current fees structure')
BEGIN
	INSERT INTO [dbo].[AIChatbotPrompts] (Area, PromptName, PromptDescription, IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES ('SQL', 'Give me the current fees structure', 'Will get the current fees structure available', 1, GETDATE(), 'System', GETDATE(), 'System')
END

IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[AIChatbotPrompts] WHERE [IsActive]=1 AND [PromptName]='Give me all the payment history data for all members')
BEGIN
	INSERT INTO [dbo].[AIChatbotPrompts] (Area, PromptName, PromptDescription, IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES ('SQL', 'Give me all the payment history data for all members', 'Will get the list of all payment history data for all present members', 1, GETDATE(), 'System', GETDATE(), 'System')
END