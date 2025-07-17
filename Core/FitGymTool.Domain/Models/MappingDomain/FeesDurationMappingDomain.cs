// *********************************************************************************
// <copyright file="FeesDurationMappingDomain.cs" company="Personal">
//     Copyright (c) 2025 Personal
// </copyright>
// <summary>The Fees Duration Mapping Domain Class.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.Models.MappingDomain;

/// <summary>
/// The Fees Duration Mapping Domain Class.
/// </summary>
/// <seealso cref="BaseDomain" />
public class FeesDurationMappingDomain : BaseDomain
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
