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
		/// The get payment history for member procedure
		/// </summary>
		public const string GetPaymentHistoryForMember_SP = "[dbo].[SP_GetPaymentHistoryForMember]";
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
