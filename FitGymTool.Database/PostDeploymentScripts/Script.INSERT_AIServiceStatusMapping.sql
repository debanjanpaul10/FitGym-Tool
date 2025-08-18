IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[AIServiceStatusMapping] WHERE [IsActive]=1 AND [StatusName]='Active')
BEGIN
	INSERT INTO [dbo].[AIServiceStatusMapping] ([StatusName], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy])
	VALUES ('Active', 1, GETUTCDATE(), 'System', GETUTCDATE(), 'System')
END

IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[AIServiceStatusMapping] WHERE [IsActive]=1 AND [StatusName]='Disabled')
BEGIN
	INSERT INTO [dbo].[AIServiceStatusMapping] ([StatusName], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy])
	VALUES ('Disabled', 1, GETUTCDATE(), 'System', GETUTCDATE(), 'System')
END

IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[AIServiceStatusMapping] WHERE [IsActive]=1 AND [StatusName]='Decommissioned')
BEGIN
	INSERT INTO [dbo].[AIServiceStatusMapping] ([StatusName], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy])
	VALUES ('Decommissioned', 1, GETUTCDATE(), 'System', GETUTCDATE(), 'System')
END