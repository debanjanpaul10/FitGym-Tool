// *********************************************************************************
//	<copyright file="UpdateMembershipStatusDTO.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>Update Membership Status Data DTO.</summary>
// *********************************************************************************
namespace FitGymTool.API.Adapters.Models.Request;

/// <summary>
/// Update Membership Status Data DTO.
/// </summary>
public class UpdateMembershipStatusDTO : BaseDTO
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
}
