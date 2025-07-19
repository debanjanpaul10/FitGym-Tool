// *********************************************************************************
//	<copyright file="MembershipStatusMappingDomain.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Membership Status Mapping Domain.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.Models.MappingDomain;

/// <summary>
/// The Membership Status Mapping Domain.
/// </summary>
/// <seealso cref="FitGymTool.Domain.Models.BaseDomain" />
public class MembershipStatusMappingDomain : BaseDomain
{
	/// <summary>
	/// Gets or sets the Id (identity column).
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the Status Name.
	/// </summary>
	public string StatusName { get; set; } = string.Empty;
}
