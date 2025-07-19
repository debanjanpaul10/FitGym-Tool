// *********************************************************************************
//	<copyright file="MappingMasterDataDomain.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Mapping Master Data Domain.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.Models.MappingDomain;

/// <summary>
/// The Mapping Master Data Domain.
/// </summary>
public class MappingMasterDataDomain
{
	/// <summary>
	/// Gets or sets the fees duration mapping.
	/// </summary>
	/// <value>
	/// The fees duration mapping.
	/// </value>
	public IEnumerable<FeesDurationMappingDomain> FeesDurationMapping { get; set; } = [];

	/// <summary>
	/// Gets or sets the fees payment status mapping.
	/// </summary>
	/// <value>
	/// The fees payment status mapping.
	/// </value>
	public IEnumerable<FeesPaymentStatusMappingDomain> FeesPaymentStatusMapping { get; set; } = [];

	/// <summary>
	/// Gets or sets the membership status mapping.
	/// </summary>
	/// <value>
	/// The membership status mapping.
	/// </value>
	public IEnumerable<MembershipStatusMappingDomain> MembershipStatusMapping { get; set; } = [];

	/// <summary>
	/// Gets or sets the bug severity mapping.
	/// </summary>
	/// <value>
	/// The bug severity mapping.
	/// </value>
	public IEnumerable<BugSeverityMappingDomain> BugSeverityMapping { get; set; } = [];
}
