IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.FeesPaymentStatusMapping WHERE IsActive=1 AND StatusName='Paid')
BEGIN
	INSERT INTO dbo.FeesPaymentStatusMapping (StatusName, IsActive, DateCreated, CreatedBy)
	VALUES ('Paid', 1, GETUTCDATE(), 'System')
END
GO;

IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.FeesPaymentStatusMapping WHERE IsActive=1 AND StatusName='Due')
BEGIN
	INSERT INTO dbo.FeesPaymentStatusMapping (StatusName, IsActive, DateCreated, CreatedBy)
	VALUES ('Due', 1, GETUTCDATE(), 'System')
END
GO;

IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.FeesPaymentStatusMapping WHERE IsActive=1 AND StatusName='Overdue')
BEGIN
	INSERT INTO dbo.FeesPaymentStatusMapping (StatusName, IsActive, DateCreated, CreatedBy)
	VALUES ('Overdue', 1, GETUTCDATE(), 'System')
END
GO;

IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.FeesPaymentStatusMapping WHERE IsActive=1 AND StatusName='To Be Cancelled')
BEGIN
	INSERT INTO dbo.FeesPaymentStatusMapping (StatusName, IsActive, DateCreated, CreatedBy)
	VALUES ('To Be Cancelled', 1, GETUTCDATE(), 'System')
END
GO;
