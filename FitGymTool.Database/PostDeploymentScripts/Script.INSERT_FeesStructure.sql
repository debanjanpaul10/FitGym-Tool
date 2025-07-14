IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.FeesStructure WHERE IsActive=1 AND FeesDurationId=1)
BEGIN
	INSERT INTO dbo.FeesStructure(FeesDurationId, FeesAmount, IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES (1, 1300.00, 1, GETUTCDATE(), 'Admin', GETUTCDATE(), 'Admin')
END

IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.FeesStructure WHERE IsActive=1 AND FeesDurationId=2)
BEGIN
	INSERT INTO dbo.FeesStructure(FeesDurationId, FeesAmount, IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES (2, 3500.00, 1, GETUTCDATE(), 'Admin', GETUTCDATE(), 'Admin')
END

IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.FeesStructure WHERE IsActive=1 AND FeesDurationId=3)
BEGIN
	INSERT INTO dbo.FeesStructure(FeesDurationId, FeesAmount, IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES (3, 5500.00, 1, GETUTCDATE(), 'Admin', GETUTCDATE(), 'Admin')
END

IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.FeesStructure WHERE IsActive=1 AND FeesDurationId=4)
BEGIN
	INSERT INTO dbo.FeesStructure(FeesDurationId, FeesAmount, IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES (4, 9500.00, 1, GETUTCDATE(), 'Admin', GETUTCDATE(), 'Admin')
END