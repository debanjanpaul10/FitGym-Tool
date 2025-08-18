CREATE TYPE [dbo].[AddNewMemberTableType] AS TABLE
(
	[MemberEmail] NVARCHAR(MAX),
	[MemberName] NVARCHAR(MAX),
	[MemberPhoneNumber] NVARCHAR(MAX),
	[MemberAddress] NVARCHAR(MAX),
	[MemberGender] NVARCHAR(10),
	[MemberJoinDate] DATE,
	[MemberDateOfBirth] DATE,
	[FeesDurationTypeName] NVARCHAR(20),
	[CreatedBy] NVARCHAR(MAX)
)
