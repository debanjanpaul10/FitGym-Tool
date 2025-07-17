// *********************************************************************************
//	<copyright file="AddMemberDomain.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Add Member Domain Class.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.Models.Members;

/// <summary>
/// Add Member Domain Class.
/// </summary>
/// <seealso cref="FitGymTool.Domain.Models.BaseDomain" />
public class AddMemberDomain : BaseDomain
{
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
	public DateTime MemberDateOfBirth { get; set; } = DateTime.UtcNow;

	/// <summary>
	/// Gets or sets the Member Gender.
	/// </summary>
	public string MemberGender { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the Member Join Date.
	/// </summary>
	public DateTime MemberJoinDate { get; set; } = DateTime.UtcNow;

	/// <summary>
	/// Gets or sets the Membership Status.
	/// </summary>
	public string MembershipStatus { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the Member GUID.
	/// </summary>
	public Guid MemberGuid { get; set; } = new Guid();

	/// <summary>
	/// Ensures all DateTime fields are set to valid values.
	/// </summary>
	public void EnsureValidDates()
	{
		if (MemberDateOfBirth == DateTime.MinValue)
		{
			MemberDateOfBirth = DateTime.UtcNow;
		}
		if (MemberJoinDate == DateTime.MinValue)
		{
			MemberJoinDate = DateTime.UtcNow;
		}
		if (DateCreated == DateTime.MinValue)
		{
			DateCreated = DateTime.UtcNow;
		}
		if (DateModified == DateTime.MinValue)
		{
			DateModified = DateTime.UtcNow;
		}
	}
}
