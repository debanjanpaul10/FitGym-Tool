// *********************************************************************************
//	<copyright file="MemberDetailsDomain.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Member Details Domain Class.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.Models.Members;

/// <summary>
/// The Member Details Domain Class.
/// </summary>
/// <seealso cref="BaseDomain" />
public class MemberDetailsDomain : BaseDomain
{
	/// <summary>
	/// Gets or sets the Member ID.
	/// </summary>
	public int MemberId { get; set; }

	/// <summary>
	/// Gets or sets the Member GUID.
	/// </summary>
	public Guid MemberGuid { get; set; }

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
	public string MembershipStatus { get; set; } = string.Empty;
}
