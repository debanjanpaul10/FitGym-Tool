// *********************************************************************************
// <copyright file="FeesDurationMapping.cs" company="Personal">
//     Copyright (c) 2025 Personal
// </copyright>
// <summary>The Fees Duration Mapping Data DTO.</summary>
// *********************************************************************************

namespace FitGymTool.API.Adapters.Models.Response.MappingData;

/// <summary>
/// The Fees Duration Mapping Data DTO.
/// </summary>
public class FeesDurationMappingDto
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the name of the duration type.
	/// </summary>
	/// <value>
	/// The name of the duration type.
	/// </value>
	public string DurationTypeName { get; set; } = string.Empty;
}
