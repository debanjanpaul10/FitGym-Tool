// *********************************************************************************
//	<copyright file="MemberDetails.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Member Details Entity Class.</summary>
// *********************************************************************************

using FitGymTool.Persistence.Adapters.Entity.Mapping;

namespace FitGymTool.Persistence.Adapters.Entity;

/// <summary>
/// The Member Details Entity Class.
/// </summary>
/// <seealso cref="BaseEntity"/>
public class MemberDetails : BaseEntity
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
	public DateTime MemberDateOfBirth { get; set; } = DateTime.Now;

	/// <summary>
	/// Gets or sets the Member Gender.
	/// </summary>
	public string MemberGender { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the Member Join Date.
	/// </summary>
	public DateTime MemberJoinDate { get; set; } = DateTime.UtcNow;

	/// <summary>
	/// Gets or sets the Membership Status ID.
	/// </summary>
	public int MembershipStatusId { get; set; }

	/// <summary>
	/// Gets or sets the Member GUID.
	/// </summary>
	public Guid MemberGuid { get; set; } = Guid.Empty;

	#region Navigation Fields

	/// <summary>
	/// Gets or sets the membership status mapping.
	/// </summary>
	/// <value>
	/// The membership status mapping.
	/// </value>
	public MembershipStatusMapping? MembershipStatusMapping { get; set; }

	#endregion
}
