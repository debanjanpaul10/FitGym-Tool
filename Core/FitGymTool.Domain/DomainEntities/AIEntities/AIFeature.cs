// *********************************************************************************
//	<copyright file="AIFeature.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The AI Feature Domain class.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.DomainEntities.AIEntities;

/// <summary>
/// The AI Feature Domain class.
/// </summary>
public class AIFeature : BaseEntity
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the name of the service.
	/// </summary>
	/// <value>
	/// The name of the service.
	/// </value>
	public string ServiceName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets a value indicating whether [service status].
	/// </summary>
	/// <value>
	///   <c>true</c> if [service status]; otherwise, <c>false</c>.
	/// </value>
	public bool ServiceStatus { get; set; }
}
