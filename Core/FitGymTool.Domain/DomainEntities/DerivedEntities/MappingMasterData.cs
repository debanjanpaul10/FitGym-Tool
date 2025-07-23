// *********************************************************************************
//	<copyright file="MappingMasterData.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Mapping Master Data.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities.Mapping;

namespace FitGymTool.Domain.DomainEntities.DerivedEntities;

/// <summary>
/// The Mapping Master Data.
/// </summary>
public class MappingMasterData
{
	/// <summary>
	/// Gets or sets the fees duration mapping.
	/// </summary>
	/// <value>
	/// The fees duration mapping.
	/// </value>
	public IEnumerable<FeesDurationMapping> FeesDurationMapping { get; set; } = [];

	/// <summary>
	/// Gets or sets the fees payment status mapping.
	/// </summary>
	/// <value>
	/// The fees payment status mapping.
	/// </value>
	public IEnumerable<FeesPaymentStatusMapping> FeesPaymentStatusMapping { get; set; } = [];

	/// <summary>
	/// Gets or sets the membership status mapping.
	/// </summary>
	/// <value>
	/// The membership status mapping.
	/// </value>
	public IEnumerable<MembershipStatusMapping> MembershipStatusMapping { get; set; } = [];

	/// <summary>
	/// Gets or sets the bug severity mapping.
	/// </summary>
	/// <value>
	/// The bug severity mapping.
	/// </value>
	public IEnumerable<BugSeverityMapping> BugSeverityMapping { get; set; } = [];

}
