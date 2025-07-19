// *********************************************************************************
//	<copyright file="FeesPaymentStatusMappingDomain.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Fees Payment Status Mapping Domain.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.Models.MappingDomain;

/// <summary>
/// The Fees Payment Status Mapping Domain.
/// </summary>
/// <seealso cref="FitGymTool.Domain.Models.BaseDomain" />
public class FeesPaymentStatusMappingDomain : BaseDomain
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the name of the status.
	/// </summary>
	/// <value>
	/// The name of the status.
	/// </value>
	public string StatusName { get; set; } = string.Empty;
}
