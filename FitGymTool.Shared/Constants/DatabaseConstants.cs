// *********************************************************************************
//	<copyright file="DatabaseConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Database Constants Class.</summary>
// *********************************************************************************

namespace FitGymTool.Shared.Constants;

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

}
