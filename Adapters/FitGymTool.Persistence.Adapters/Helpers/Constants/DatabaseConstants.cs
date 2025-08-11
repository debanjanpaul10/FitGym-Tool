// *********************************************************************************
//	<copyright file="DatabaseConstants.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Database Constants Class.</summary>
// *********************************************************************************

namespace FitGymTool.Persistence.Adapters.Helpers.Constants;

/// <summary>
/// The Database Constants Class.
/// </summary>
public static class DatabaseConstants
{
	/// <summary>
	/// The SQL query execution constants.
	/// </summary>
	public static class SqlQueryExecutionConstants
	{
		/// <summary>
		/// The execute function get current fees and revenue status
		/// </summary>
		public const string Execute_FN_GetCurrentFeesAndRevenueStatus = "SELECT * FROM dbo.FN_GetCurrentFeesAndRevenueStatus()";

		/// <summary>
		/// The execute function get current members fees status
		/// </summary>
		public const string Execute_FN_GetCurrentMembersFeesStatus = "SELECT * FROM dbo.FN_GetCurrentMembersFeesStatus()";
	}

	/// <summary>
	/// The Stored Procedure constants.
	/// </summary>
	public static class StoredProceduresConstants
	{
		/// <summary>
		/// The Stored Procedures Names Constants.
		/// </summary>
		public static class StoredProcedure_Names
		{
			/// <summary>
			/// The get payment history for member procedure
			/// </summary>
			public const string GetPaymentHistoryForMember_SP = "EXEC [dbo].[SP_GetPaymentHistoryForMember] @MemberEmailId = {0}";

			/// <summary>
			/// The add new member data sp
			/// </summary>
			public const string AddNewMemberData_SP = "EXEC [dbo].[SP_AddNewMemberData] @NewMemberData";

			/// <summary>
			/// The update member fees data sp
			/// </summary>
			public const string UpdateMemberFeesData_SP = "EXEC [dbo].[SP_UpdateMemberFeesData] @UpdateMemberFeesData";
		}

		/// <summary>
		/// The Stored Procedures Inputs Constants.
		/// </summary>
		internal static class StoredProcedures_Inputs
		{
			/// <summary>
			/// Creates new memberdatainput.
			/// </summary>
			internal const string NewMemberDataInput = "@NewMemberData";

			/// <summary>
			/// The member email
			/// </summary>
			internal const string MemberEmail = "MemberEmail";

			/// <summary>
			/// The member name
			/// </summary>
			internal const string MemberName = "MemberName";

			/// <summary>
			/// The member phone number
			/// </summary>
			internal const string MemberPhoneNumber = "MemberPhoneNumber";

			/// <summary>
			/// The member address
			/// </summary>
			internal const string MemberAddress = "MemberAddress";

			/// <summary>
			/// The member gender
			/// </summary>
			internal const string MemberGender = "MemberGender";

			/// <summary>
			/// The member join date
			/// </summary>
			internal const string MemberJoinDate = "MemberJoinDate";

			/// <summary>
			/// The member date of birth
			/// </summary>
			internal const string MemberDateOfBirth = "MemberDateOfBirth";

			/// <summary>
			/// The fees duration type name
			/// </summary>
			internal const string FeesDurationTypeName = "FeesDurationTypeName";

			/// <summary>
			/// The created by
			/// </summary>
			internal const string CreatedBy = "CreatedBy";

			/// <summary>
			/// The update member fees data input
			/// </summary>
			internal const string UpdateMemberFeesDataInput = "@UpdateMemberFeesData";

			/// <summary>
			/// The amount
			/// </summary>
			internal const string Amount = "Amount";

			/// <summary>
			/// From date
			/// </summary>
			internal const string FromDate = "FromDate";

			/// <summary>
			/// Converts to date.
			/// </summary>
			internal const string ToDate = "ToDate";

			/// <summary>
			/// The modified by
			/// </summary>
			internal const string ModifiedBy = "ModifiedBy";
		}
	}

	/// <summary>
	/// The Table Constants Class.
	/// </summary>
	internal static class TableConstants
	{
		/// <summary>
		/// The add new member table type
		/// </summary>
		internal const string AddNewMemberTableType = "dbo.AddNewMemberTableType";

		/// <summary>
		/// The update member fees table type
		/// </summary>
		internal const string UpdateMemberFeesTableType = "dbo.UpdateMemberFeesTableType";
	}

	/// <summary>
	/// The error messages class.
	/// </summary>
	public static class ErrorMessages
	{
		/// <summary>
		/// The database connection not found
		/// </summary>
		public const string DatabaseConnectionNotFound = "Oops! It seems the database connection is missing from the configuration!";
	}

	/// <summary>
	/// The Configuration Constants Class.
	/// </summary>
	public static class ConfigurationConstants
	{
		/// <summary>
		/// The local SQL database connection string constant.
		/// </summary>
		public const string LocalSqlConnectionStringConstant = "LocalSqlConnectionString";

		/// <summary>
		/// The Azure SQL database connection string constant.
		/// </summary>
		public const string AzureSqlConnectionStringConstant = "AzureSqlConnectionString";
	}

	/// <summary>
	/// The Logging Constants Class.
	/// </summary>
	public static class LoggingConstants
	{
		/// <summary>
		/// The method started message constant
		/// </summary>
		public static readonly string MethodStartedMessageConstant = "Method {0} started at {1} for {2}";

		/// <summary>
		/// The method ended message constant
		/// </summary>
		public static readonly string MethodEndedMessageConstant = "Method {0} ended at {1} for {2}";

		/// <summary>
		/// The method failed with message constant.
		/// </summary>
		/// <returns>{0} failed at {1} with {2}</returns>
		public const string MethodFailedWithMessageConstant = "Method {0} failed at {1} with {2}";
	}

	/// <summary>
	/// The constant for the Not Applicable string value.
	/// </summary>
	public const string NotApplicableStringConstant = "NA";

	/// <summary>
	/// The medium constant
	/// </summary>
	public const string MediumConstant = "Medium";

	/// <summary>
	/// The not started constant
	/// </summary>
	public const string NotStartedConstant = "Not Started";
}
