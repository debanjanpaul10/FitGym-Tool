// *********************************************************************************
// <copyright file="BugItemStatusMapping.cs" company="Personal">
//     Copyright (c) 2025 <Debanjan's Lab>
// </copyright>
// <summary>The Bug Severity Mapping.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.Models.MappingDomain;

/// <summary>
/// The Bug Severity Mapping.
/// </summary>
/// <seealso cref="BaseEntity"/>
public class BugSeverityMappingDomain : BaseDomain
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
