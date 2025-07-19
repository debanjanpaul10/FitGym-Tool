// *********************************************************************************
//	<copyright file="IMembersHandler.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Members Handler Adapter Interface.</summary>
// *********************************************************************************

using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.Domain.Models.Members;

namespace FitGymTool.API.Adapters.Contracts;

/// <summary>
/// The Members Handler Adapter Interface.
/// </summary>
public interface IMembersHandler
{
	/// <summary>
	/// Adds a new member to the database asynchronously.
	/// </summary>
	/// <param name="memberDetails">The member details data.</param>
	/// <param name="isFromAdmin">The boolean flag to indicate admin request.</param>
	/// <param name="userEmail">The user email.</param>
	/// <returns>The boolean result for success/failure.</returns>
	Task<bool> AddNewMemberAsync(AddMemberDTO memberDetails, string userEmail, bool isFromAdmin);

	/// <summary>
	/// Gets all members from the database asynchronously.
	/// </summary>
	/// <returns>A list of MemberDetails.</returns>
	Task<List<MemberDetailsDTO>> GetAllMembersAsync();

	/// <summary>
	/// Gets a single member's details by Member's Email ID. asynchronously.
	/// </summary>
	/// <param name="memberEmail">The member's Email ID.</param>
	/// <returns>The MemberDetails object if found; otherwise, null.</returns>
	Task<MemberDetailsDTO> GetMemberByEmailIdAsync(string memberEmail);

	/// <summary>
	/// Updates an existing member's details asynchronously.
	/// </summary>
	/// <param name="memberDetails">The updated member details.</param>
	/// <returns>The boolean result for success/failure.</returns>
	Task<bool> UpdateMemberDetailsAsync(UpdateMemberDTO memberDetails);

	/// <summary>
	/// Updates the membership status asynchronous.
	/// </summary>
	/// <param name="updateMembershipStatusDto">The update membership status dto.</param>
	/// <returns>The boolean result for success/failure.</returns>
	Task<bool> UpdateMembershipStatusAsync(UpdateMembershipStatusDTO updateMembershipStatusDto);
}
