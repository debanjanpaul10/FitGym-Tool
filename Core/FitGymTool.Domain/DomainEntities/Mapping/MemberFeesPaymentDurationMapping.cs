// *********************************************************************************
//	<copyright file="MemberFeesPaymentDurationMapping.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Member Fees Payment Duration Mapping Entity.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.DomainEntities.Mapping;

/// <summary>
/// The Member Fees Payment Duration Mapping Entity.
/// </summary>
/// <seealso cref="FitGymTool.Domain.DomainEntities.BaseEntity" />
public class MemberFeesPaymentDurationMapping : BaseEntity
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
	/// Gets or sets the fees duration identifier.
	/// </summary>
	/// <value>
	/// The fees duration identifier.
	/// </value>
	public int FeesDurationId { get; set; }

	#region NAVIGATION MAPPING

	/// <summary>
	/// Gets or sets the fees duration mapping.
	/// </summary>
	/// <value>
	/// The fees duration mapping.
	/// </value>
	public FeesDurationMapping? FeesDurationMapping { get; set; } = new FeesDurationMapping();

	/// <summary>
	/// Gets or sets the member details.
	/// </summary>
	/// <value>
	/// The member details.
	/// </value>
	public MemberDetails? MemberDetails { get; set; } = new MemberDetails();

	#endregion
}
