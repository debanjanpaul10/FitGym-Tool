// *********************************************************************************
//	<copyright file="UpdateMemberDTO.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>UpdateMemberDTO is a Data Transfer Object (DTO) used to update member details.</summary>
// *********************************************************************************

namespace FitGymTool.API.Adapters.Models.Request;

/// <summary>
/// UpdateMemberDTO is a Data Transfer Object (DTO) used to update member details.
/// </summary>
public class UpdateMemberDTO : BaseDTO
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
	/// Gets or sets the member join date.
	/// </summary>
	/// <value>
	/// The member join date.
	/// </value>
	public DateTime MemberJoinDate { get; set; }

	/// <summary>
	/// Gets or sets the Member Gender.
	/// </summary>
	public string MemberGender { get; set; } = string.Empty;
}
