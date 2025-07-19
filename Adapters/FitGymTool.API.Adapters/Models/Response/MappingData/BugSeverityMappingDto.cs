// *********************************************************************************
//	<copyright file="BugSeverityMappingDto.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Bug Severity Mapping Data DTO.</summary>
// *********************************************************************************

namespace FitGymTool.API.Adapters.Models.Response.MappingData;

/// <summary>
/// The Bug Severity Mapping Data DTO.
/// </summary>
public class BugSeverityMappingDto
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the name of the severity.
	/// </summary>
	/// <value>
	/// The name of the severity.
	/// </value>
	public string SeverityName { get; set; } = string.Empty;
}
