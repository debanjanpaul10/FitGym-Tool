// *********************************************************************************
//	<copyright file="MemberDetails.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Member Details Entity Class.</summary>
// *********************************************************************************

namespace FitGymTool.DataAccess.Entity;

/// <summary>
/// The Member Details Entity Class.
/// </summary>
public class MemberDetails
{
	/// <summary>
	/// Gets or sets the Member ID.
	/// </summary>
	public int MemberId { get; set; }

	/// <summary>
	/// Gets or sets the Member Name.
	/// </summary>
	public string MemberName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the Member Email.
	/// </summary>
	public string MemberEmail { get; set; } = string.Empty;

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
	public int MembershipStatus { get; set; }

	/// <summary>
	/// Gets or sets the Member GUID.
	/// </summary>
	public Guid MemberGuid { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this member is active.
	/// </summary>
	public bool IsActive { get; set; }

	/// <summary>
	/// Gets or sets the date created.
	/// </summary>
	/// <value>
	/// The date created.
	/// </value>
	public DateTime DateCreated { get; set; }

	/// <summary>
	/// Gets or sets the date modified.
	/// </summary>
	/// <value>
	/// The date modified.
	/// </value>
	public DateTime DateModified { get; set; }
}
