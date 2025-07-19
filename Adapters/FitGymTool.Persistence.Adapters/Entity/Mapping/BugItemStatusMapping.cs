// *********************************************************************************
// <copyright file="BugItemStatusMapping.cs" company="Personal">
//     Copyright (c) 2025 <Debanjan's Lab>
// </copyright>
// <summary>The Bug Item Status Mapping Entity Class.</summary>
// *********************************************************************************
namespace FitGymTool.Persistence.Adapters.Entity.Mapping;

/// <summary>
/// The Bug Item Status Mapping Entity Class.
/// </summary>
/// <seealso cref="BaseEntity"/>
public class BugItemStatusMapping : BaseEntity
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
