// *********************************************************************************
//	<copyright file="IMembersDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Data Service Interface.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.Models.Members;

/// <summary>
/// The Update Membership Status Domain.
/// </summary>
public class UpdateMembershipStatusDomain
{
	/// <summary>
	/// Gets or sets the member identifier.
	/// </summary>
	/// <value>
	/// The member identifier.
	/// </value>
	public int MemberId { get; set; }

	/// <summary>
	/// Gets or sets the membership status identifier.
	/// </summary>
	/// <value>
	/// The membership status identifier.
	/// </value>
	public int MembershipStatusId { get; set; }

	/// <summary>
	/// Gets or sets the member email address.
	/// </summary>
	/// <value>
	/// The member email address.
	/// </value>
	public string MemberEmailAddress { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the modified by.
	/// </summary>
	/// <value>
	/// The modified by.
	/// </value>
	public string ModifiedBy { get; set; } = string.Empty;
}
