// *********************************************************************************
// <copyright file="MembershipStatusMappingDto.cs" company="Personal">
//     Copyright (c) 2025 <Debanjan's Lab>
// </copyright>
// <summary>The Membership Status Mapping Data DTO.</summary>
// *********************************************************************************

namespace FitGymTool.API.Adapters.Models.Response.MappingData;

/// <summary>
/// The Membership Status Mapping Data DTO.
/// </summary>
public class MembershipStatusMappingDto
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
