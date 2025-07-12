/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
			   SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.MembershipStatusMapping WHERE IsActive=1 AND StatusName='Active')
BEGIN
	INSERT INTO dbo.MembershipStatusMapping (StatusName, StatusId, IsActive, DateCreated)
	VALUES ('Active', 1, 1, GETUTCDATE())
END
GO;

IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.MembershipStatusMapping WHERE IsActive=1 AND StatusName='On Termination')
BEGIN
	INSERT INTO dbo.MembershipStatusMapping (StatusName, StatusId, IsActive, DateCreated)
	VALUES ('On Termination', 2, 1, GETUTCDATE())
END
GO;

IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.MembershipStatusMapping WHERE IsActive=1 AND StatusName='Expired')
BEGIN
	INSERT INTO dbo.MembershipStatusMapping (StatusName, StatusId, IsActive, DateCreated)
	VALUES ('Expired', 3, 1, GETUTCDATE())
END
GO;