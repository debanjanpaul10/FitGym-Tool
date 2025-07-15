// *********************************************************************************
//	<copyright file="IMembersDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Data Service Interface.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.DrivenPorts;

/// <summary>
/// The Members Data Service Interface.
/// </summary>
public interface IMembersManager
{
	/// <summary>
	/// Adds a new member to the database asynchronously.
	/// </summary>
	/// <param name="memberDetails">The member details data.</param>
	/// <returns>The boolean result for success/failure.</returns>
	Task<bool> AddNewMemberAsync(MemberDetails memberDetails);

	/// <summary>
	/// Gets all members from the database asynchronously.
	/// </summary>
	/// <returns>A list of MemberDetails.</returns>
	Task<List<MemberDetails>> GetAllMembersAsync();

	/// <summary>
	/// Gets a single member's details by Member's Email ID asynchronously.
	/// </summary>
	/// <param name="memberEmail">The member's Email ID.</param>
	/// <returns>The MemberDetails object if found; otherwise, null.</returns>
	Task<MemberDetails?> GetMemberByEmailIdAsync(string memberEmail);

	/// <summary>
	/// Updates an existing member's details asynchronously.
	/// </summary>
	/// <param name="memberDetails">The updated member details.</param>
	/// <returns>The boolean result for success/failure.</returns>
	Task<bool> UpdateMemberAsync(MemberDetails memberDetails);

	/// <summary>
	/// Deletes a member by MemberId asynchronously.
	/// </summary>
	/// <param name="memberId">The member's ID.</param>
	/// <returns>The boolean result for success/failure.</returns>
	Task<bool> DeleteMemberAsync(int memberId);
}
