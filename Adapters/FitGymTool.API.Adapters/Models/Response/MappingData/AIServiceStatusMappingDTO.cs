// *********************************************************************************
//	<copyright file="AIServiceStatusMappingDTO.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The AI Service Status Mapping Data DTO.</summary>
// *********************************************************************************

namespace FitGymTool.API.Adapters.Models.Response.MappingData;

/// <summary>
/// The AI Service Status Mapping Data DTO.
/// </summary>
public class AIServiceStatusMappingDTO
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
