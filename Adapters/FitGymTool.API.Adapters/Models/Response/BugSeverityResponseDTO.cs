// *********************************************************************************
//	<copyright file="BugSeverityResponseDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Bug Severity Response DTO Class.</summary>
// *********************************************************************************

namespace FitGymTool.API.Adapters.Models.Response;

/// <summary>
/// The Bug Severity Response DTO.
/// </summary>
public class BugSeverityResponseDTO
{
	/// <summary>
	/// Gets or sets the bug severity.
	/// </summary>
	/// <value>
	/// The bug severity.
	/// </value>
	public string BugSeverity { get; set; } = string.Empty;
}
