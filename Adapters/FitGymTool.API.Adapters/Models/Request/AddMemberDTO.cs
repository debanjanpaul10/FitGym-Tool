// *********************************************************************************
//	<copyright file="AddMemberDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Add Member Data Transfer Object Class.</summary>
// *********************************************************************************

namespace FitGymTool.API.Adapters.Models.Request;

/// <summary>
/// The Add Member Data Transfer Object Class.
/// </summary>
public class AddMemberDTO
{
	/// <summary>
	/// Gets or sets the Member Name.
	/// </summary>
	public string MemberName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the Member Email Address.
	/// If added by admin, only then has non-null value.
	/// </summary>
	public string? MemberEmail { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the Member Phone Number.
	/// </summary>
	public string MemberPhoneNumber { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the Member Address.
	/// </summary>
	public string MemberAddress { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the Member Date of Birth.
	/// </summary>
	public DateTime MemberDateOfBirth { get; set; }

	/// <summary>
	/// Gets or sets the Member Gender.
	/// </summary>
	public string MemberGender { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the Member Join Date.
	/// </summary>
	public DateTime MemberJoinDate { get; set; }

	/// <summary>
	/// Gets or sets the Membership Status.
	/// </summary>
	public string MembershipStatus { get; set; } = string.Empty;
}
