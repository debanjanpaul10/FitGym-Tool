// *********************************************************************************
// <copyright file="FeesDurationMapping.cs" company="Personal">
//     Copyright (c) 2025 <Debanjan's Lab>
// </copyright>
// <summary>The Fees Duration Mapping Entity Class.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.DomainEntities.Mapping;

/// <summary>
/// The Fees Duration Mapping Entity Class.
/// </summary>
/// <seealso cref="BaseEntity"/>
public class FeesDurationMapping : BaseEntity
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
