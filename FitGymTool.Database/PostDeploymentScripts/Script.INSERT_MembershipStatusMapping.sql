IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.MembershipStatusMapping WHERE IsActive=1 AND StatusName='Active')
BEGIN
	INSERT INTO dbo.MembershipStatusMapping (StatusName, IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES ('Active', 1, GETUTCDATE(), 'System', GETUTCDATE(), 'System')
END


IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.MembershipStatusMapping WHERE IsActive=1 AND StatusName='On Termination')
BEGIN
	INSERT INTO dbo.MembershipStatusMapping (StatusName, IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES ('On Termination', 1, GETUTCDATE(),'System', GETUTCDATE(), 'System')
END


IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.MembershipStatusMapping WHERE IsActive=1 AND StatusName='Expired')
BEGIN
	INSERT INTO dbo.MembershipStatusMapping (StatusName, IsActive, DateCreated, CreatedBy, DateModified, ModifiedBy)
	VALUES ('Expired', 1, GETUTCDATE(),'System', GETUTCDATE(), 'System')
END
