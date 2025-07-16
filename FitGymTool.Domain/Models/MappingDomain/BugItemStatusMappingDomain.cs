// *********************************************************************************
// <copyright file="BugItemStatusMapping.cs" company="Personal">
//     Copyright (c) 2025 Personal
// </copyright>
// <summary>The Bug Item Status Mapping Entity Class.</summary>
// *********************************************************************************
namespace FitGymTool.Domain.Models.MappingDomain;

/// <summary>
/// The Bug Item Status Mapping Entity Class.
/// </summary>
/// <seealso cref="BaseEntity"/>
public class BugItemStatusMappingDomain : BaseDomain
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
