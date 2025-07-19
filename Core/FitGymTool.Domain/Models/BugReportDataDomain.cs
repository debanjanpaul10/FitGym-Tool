// *********************************************************************************
//	<copyright file="BugReportData.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Bug Report Data Entity Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.Models.MappingDomain;

namespace FitGymTool.Domain.Models;

/// <summary>
/// The Bug Report Data Entity Class.
/// </summary>
/// <seealso cref="BaseDomain" />
public class BugReportDataDomain : BaseDomain
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the title.
	/// </summary>
	/// <value>
	/// The title.
	/// </value>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	/// <value>
	/// The description.
	/// </value>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the bug severity identifier.
	/// </summary>
	/// <value>
	/// The bug severity identifier.
	/// </value>
	public int BugSeverityId { get; set; }

	/// <summary>
	/// Gets or sets the bug status identifier.
	/// </summary>
	/// <value>
	/// The bug status identifier.
	/// </value>
	public int BugStatusId { get; set; }

	/// <summary>
	/// Gets or sets the page URL.
	/// </summary>
	/// <value>
	/// The page URL.
	/// </value>
	public string PageUrl { get; set; } = string.Empty;


	#region NAVIGATION FIELDS

	/// <summary>
	/// Gets or sets the bug item status mapping.
	/// </summary>
	/// <value>
	/// The bug item status mapping.
	/// </value>
	public BugItemStatusMappingDomain? BugItemStatusMapping { get; set; } = new BugItemStatusMappingDomain();

	/// <summary>
	/// Gets or sets the bug severity mapping.
	/// </summary>
	/// <value>
	/// The bug severity mapping.
	/// </value>
	public BugSeverityMappingDomain? BugSeverityMapping { get; set; } = new BugSeverityMappingDomain();

	#endregion
}
