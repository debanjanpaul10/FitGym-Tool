// *********************************************************************************
//	<copyright file="BugReportData.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Bug Report Data Entity Class.</summary>
// *********************************************************************************

namespace FitGymTool.Persistence.Adapters.Entity;

/// <summary>
/// The Bug Report Data Entity Class.
/// </summary>
/// <seealso cref="FitGymTool.Infrastructure.DB.Entity.BaseEntity" />
public class BugReportData : BaseEntity
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
}
