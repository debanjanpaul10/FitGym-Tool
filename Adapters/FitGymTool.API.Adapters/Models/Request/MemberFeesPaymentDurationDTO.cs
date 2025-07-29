// *********************************************************************************
//	<copyright file="MemberFeesPaymentDurationDto.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Member Fees Payment Duration DTO.</summary>
// *********************************************************************************

namespace FitGymTool.API.Adapters.Models.Request;

/// <summary>
/// The Member Fees Payment Duration DTO.
/// </summary>
public class MemberFeesPaymentDurationDTO : BaseDTO
{
	/// <summary>
	/// Gets or sets the member identifier.
	/// </summary>
	/// <value>
	/// The member identifier.
	/// </value>
	public int MemberId { get; set; }

	/// <summary>
	/// Gets or sets the member email.
	/// </summary>
	/// <value>
	/// The member email.
	/// </value>
	public string MemberEmail { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the fees duration status.
	/// </summary>
	/// <value>
	/// The fees duration status.
	/// </value>
	public string FeesDurationStatus { get; set; } = string.Empty;
}
