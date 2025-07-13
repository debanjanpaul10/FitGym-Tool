IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.FeesPaymentStatusMapping WHERE IsActive=1 AND StatusName='Paid')
BEGIN
	INSERT INTO dbo.FeesPaymentStatusMapping (StatusName, IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES ('Paid', 1, GETUTCDATE(), 'System', GETUTCDATE(), 'System')
END


IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.FeesPaymentStatusMapping WHERE IsActive=1 AND StatusName='Due')
BEGIN
	INSERT INTO dbo.FeesPaymentStatusMapping (StatusName, IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES ('Due', 1, GETUTCDATE(), 'System', GETUTCDATE(), 'System')
END


IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.FeesPaymentStatusMapping WHERE IsActive=1 AND StatusName='Overdue')
BEGIN
	INSERT INTO dbo.FeesPaymentStatusMapping (StatusName, IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES ('Overdue', 1, GETUTCDATE(), 'System', GETUTCDATE(), 'System')
END


IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.FeesPaymentStatusMapping WHERE IsActive=1 AND StatusName='To Be Cancelled')
BEGIN
	INSERT INTO dbo.FeesPaymentStatusMapping (StatusName, IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES ('To Be Cancelled', 1, GETUTCDATE(), 'System', GETUTCDATE(), 'System')
END

