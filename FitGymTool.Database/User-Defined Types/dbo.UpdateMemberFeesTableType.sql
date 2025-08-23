CREATE TYPE [dbo].[UpdateMemberFeesTableType] AS TABLE
(
	[MemberEmail] NVARCHAR(255),
	[Amount] DECIMAL(10,2),
	[FromDate] DATE,
	[ToDate] DATE,
	[ModifiedBy] NVARCHAR(255)
)
