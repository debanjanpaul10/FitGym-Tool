// *********************************************************************************
//	<copyright file="DomainConstants.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
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

	/// <summary>
	/// The Validation Error Messages Class.
	/// </summary>
	public static class ValidationErrorMessages
	{
		/// <summary>
		/// The member details cannot be null message constant.
		/// </summary>
		public const string MemberDetailsCannotBeNull = "Member details cannot be null.";

		/// <summary>
		/// The member already exists message constant.
		/// </summary>
		public const string MemberAlreadyExistsMessageConstant = "Member with the same ID and GUID already exists.";

		/// <summary>
		/// The member not found message constant.
		/// </summary>
		public const string MemberNotFoundMessageConstant = "Member with the specified ID was not found.";

		/// <summary>
		/// The member details are invalid message constant.
		/// </summary>
		public const string MemberCouldNotBeAddedMessageConstant = "Member could not be added. Please check the details and try again.";

	}
}
