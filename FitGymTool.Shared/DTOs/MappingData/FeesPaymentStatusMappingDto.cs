// *********************************************************************************
//	<copyright file="FeesPaymentStatusMappingDto.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fees Payment Status Mapping Data DTO.</summary>
// *********************************************************************************

namespace FitGymTool.Shared.DTOs.MappingData;

/// <summary>
/// The Fees Payment Status Mapping Data DTO.
/// </summary>
public class FeesPaymentStatusMappingDto
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
