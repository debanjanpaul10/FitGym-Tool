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
	[MembershipStatus] NVARCHAR(50) NOT NULL,
	[MemberGuid] UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID()),
	[IsActive] BIT NOT NULL DEFAULT(1),
)
GO;

CREATE NONCLUSTERED INDEX IX_MemberDetails_MemberEmail ON [dbo].[MemberDetails] ([MemberEmail])
GO;

CREATE NONCLUSTERED INDEX IX_MemberDetails_MemberPhoneNumber ON [dbo].[MemberDetails] ([MemberPhoneNumber])
GO;

CREATE NONCLUSTERED INDEX IX_MemberDetails_MemberGuid ON [dbo].[MemberDetails] ([MemberGuid])
GO;
