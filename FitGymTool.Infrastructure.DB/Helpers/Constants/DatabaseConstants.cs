// *********************************************************************************
//	<copyright file="DatabaseConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Database Constants Class.</summary>
// *********************************************************************************

namespace FitGymTool.Infrastructure.DB.Helpers.Constants;

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

}
