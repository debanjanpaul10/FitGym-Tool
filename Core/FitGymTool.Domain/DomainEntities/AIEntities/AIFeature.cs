// *********************************************************************************
//	<copyright file="AIFeatureDomain.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The AI Feature Domain class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities.Mapping;

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
	/// Gets or sets the service description.
	/// </summary>
	/// <value>
	/// The service description.
	/// </value>
	public string ServiceDescription { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the service status identifier.
	/// </summary>
	/// <value>
	/// The service status identifier.
	/// </value>
	public int ServiceStatusId { get; set; }

	#region NAVIGATION FIELDS

	/// <summary>
	/// Gets or sets the ai service status mapping.
	/// </summary>
	/// <value>
	/// The ai service status mapping.
	/// </value>
	public AIServiceStatusMapping? AIServiceStatusMapping { get; set; }

	#endregion
}
