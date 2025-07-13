IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.[FeesDurationMapping] WHERE IsActive=1 AND DurationTypeName='Monthly')
BEGIN
	INSERT INTO dbo.[FeesDurationMapping] ([DurationTypeName], IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES ('Monthly', 1, GETUTCDATE(), 'System', GETUTCDATE(), 'System')
END

IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.[FeesDurationMapping] WHERE IsActive=1 AND DurationTypeName='Quarterly')
BEGIN
	INSERT INTO dbo.[FeesDurationMapping] ([DurationTypeName], IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES ('Quarterly', 1, GETUTCDATE(), 'System', GETUTCDATE(), 'System')
END

IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.[FeesDurationMapping] WHERE IsActive=1 AND DurationTypeName='Half-Yearly')
BEGIN
	INSERT INTO dbo.[FeesDurationMapping] ([DurationTypeName], IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES ('Half-Yearly', 1, GETUTCDATE(), 'System', GETUTCDATE(), 'System')
END

IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.[FeesDurationMapping] WHERE IsActive=1 AND DurationTypeName='Annually')
BEGIN
	INSERT INTO dbo.[FeesDurationMapping] ([DurationTypeName], IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES ('Annually', 1, GETUTCDATE(), 'System', GETUTCDATE(), 'System')
END