// *********************************************************************************
// <copyright file="AIServiceStatusMapping.cs" company="Personal">
//     Copyright (c) 2025 <Debanjan's Lab>
// </copyright>
// <summary>The AI Service Status Mapping Domain Class.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.DomainEntities.Mapping;

/// <summary>
/// The AI Service Status Mapping Domain Class.
/// </summary>
public class AIServiceStatusMapping : BaseEntity
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
