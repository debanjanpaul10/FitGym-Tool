// *********************************************************************************
//	<copyright file="BugSeverityResponse.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Bug Severity Response Domain Class.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.DomainEntities.AIEntities;

/// <summary>
/// The Bug Severity Response.
/// </summary>
/// <seealso cref="FitGymTool.Domain.DomainEntities.AIEntities.BaseAIResponse" />
public class BugSeverityResponse : BaseAIResponse
{
	/// <summary>
	/// Gets or sets the bug severity.
	/// </summary>
	/// <value>
	/// The bug severity.
	/// </value>
	public string BugSeverity { get; set; } = string.Empty;
}
