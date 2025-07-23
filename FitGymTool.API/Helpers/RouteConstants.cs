// *********************************************************************************
//	<copyright file="RouteConstants.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Route Constants class contains constants for API route paths.</summary>
// *********************************************************************************

namespace FitGymTool.API.Helpers;

/// <summary>
/// The Route Constants class contains constants for API route paths.
/// </summary>
public static class RouteConstants
{
	/// <summary>
	/// The Members API Routes class contains constants for member-related API route paths.
	/// </summary>
	public static class MembersApiRoutes
	{
		/// <summary>
		/// The base route for member-related API endpoints.
		/// </summary>
		public const string BaseRoute_RoutePrefix = "api/Members";

		/// <summary>
		/// The route for adding a new member to the database.
		/// </summary>
		public const string AddMember_ApiRoute = "AddMember/{isFromAdmin}";

		/// <summary>
		/// The route for getting all members from the database.
		/// </summary>
		public const string GetAllMembers_ApiRoute = "GetAllMembers";

		/// <summary>
		/// The route for getting a single member's details by Member's Email ID.
		/// </summary>
		public const string GetMemberByEmailId_ApiRoute = "GetMemberByEmailId";

		/// <summary>
		/// The route for updating an existing member's details.
		/// </summary>
		public const string UpdateMember_ApiRoute = "UpdateMemberDetails";

		/// <summary>
		/// The update membership status API route
		/// </summary>
		public const string UpdateMembershipStatus_ApiRoute = "UpdateMembershipStatus";
	}

	/// <summary>
	/// The Common API Routes class.
	/// </summary>
	public static class FitGymCommonApiRoutes
	{
		/// <summary>
		/// The base route route prefix
		/// </summary>
		public const string BaseRoute_RoutePrefix = "api/Common";

		/// <summary>
		/// The get mappings master data API route
		/// </summary>
		public const string GetMappingsMasterData_ApiRoute = "GetMappingsMasterData";

		/// <summary>
		/// The add bug report API route
		/// </summary>
		public const string AddBugReport_ApiRoute = "AddBugReport";
	}

	/// <summary>
	/// The Member Fees API Routes class.
	/// </summary>
	public static class MemberFeesApiRoutes
	{
		/// <summary>
		/// The base route route prefix
		/// </summary>
		public const string BaseRoute_RoutePrefix = "api/MemberFees";

		/// <summary>
		/// The get current month fees and revenue status API route
		/// </summary>
		public const string GetCurrentMonthFeesAndRevenueStatus_ApiRoute = "GetCurrentMonthFeesAndRevenueStatus";

		/// <summary>
		/// The get current fees structure API route
		/// </summary>
		public const string GetCurrentFeesStructure_ApiRoute = "GetCurrentFeesStructure";
	}
}
