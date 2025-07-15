// *********************************************************************************
//	<copyright file="DomainConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Domain constants Class.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.Helpers;

/// <summary>
/// The Domain Constants Class.
/// </summary>
public static class DomainConstants
{
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
	/// The Header Constants class.
	/// </summary>
	public static class HeaderConstants
	{
		/// <summary>
		/// The constant for the Not Applicable string value.
		/// </summary>
		public const string NotApplicableStringConstant = "NA";
	}
}
