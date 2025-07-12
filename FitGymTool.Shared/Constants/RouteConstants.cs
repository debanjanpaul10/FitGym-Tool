// *********************************************************************************
//	<copyright file="RouteConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Route Constants class contains constants for API route paths.</summary>
// *********************************************************************************


namespace FitGymTool.Shared.Constants;

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
		public const string UpdateMember_ApiRoute = "UpdateMember";

		/// <summary>
		/// The route for deleting a member by MemberId.
		/// </summary>
		public const string DeleteMember_ApiRoute = "DeleteMember/{memberId}";
	}
}
