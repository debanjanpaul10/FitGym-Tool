CREATE TABLE [dbo].[MemberDetails]
(
	[MemberId] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[MemberName] NVARCHAR(MAX) NOT NULL,
	[MemberEmail] NVARCHAR(MAX) NOT NULL,
	[MemberPhoneNumber] NVARCHAR(MAX) NOT NULL,
	[MemberAddress] NVARCHAR(MAX) NOT NULL,
	[MemberDateOfBirth] DATE NOT NULL,
	[MemberGender] NVARCHAR(10) NOT NULL,
	[MemberJoinDate] DATE NOT NULL,
	[MembershipStatusId] INT NOT NULL,
	[MemberGuid] UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID()),
	[IsActive] BIT NOT NULL DEFAULT(1),
	[DateCreated] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] NVARCHAR(MAX) NOT NULL,
	[DateModified] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT FK_MemberDetails_MembershipStatusId FOREIGN KEY ([MembershipStatusId]) REFERENCES [dbo].[MembershipStatusMapping]([Id])
)
GO;

CREATE NONCLUSTERED INDEX IX_MemberDetails_MemberId ON [dbo].[MemberDetails] ([MemberId])
GO;

CREATE NONCLUSTERED INDEX IX_MemberDetails_IsActive ON [dbo].[MemberDetails] ([IsActive])
GO;

CREATE NONCLUSTERED INDEX IX_MemberDetails_MemberGuid ON [dbo].[MemberDetails] ([MemberGuid])
GO;
