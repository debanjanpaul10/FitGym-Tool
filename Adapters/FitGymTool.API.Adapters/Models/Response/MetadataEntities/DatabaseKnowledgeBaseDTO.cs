// *********************************************************************************
//	<copyright file="DatabaseKnowledgeBaseDTO.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Database Knowledge Base DTO.</summary>
// *********************************************************************************

namespace FitGymTool.API.Adapters.Models.Response.MetadataEntities;

/// <summary>
/// The Database Knowledge Base DTO.
/// </summary>
public class DatabaseKnowledgeBaseDTO
{
	/// <summary>
	/// Gets or sets the name of the database.
	/// </summary>
	/// <value>
	/// The name of the database.
	/// </value>
	public string DatabaseName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	/// <value>
	/// The description.
	/// </value>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the version.
	/// </summary>
	/// <value>
	/// The version.
	/// </value>
	public string Version { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the last updated.
	/// </summary>
	/// <value>
	/// The last updated.
	/// </value>
	public string LastUpdated { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the categories.
	/// </summary>
	/// <value>
	/// The categories.
	/// </value>
	public Dictionary<string, CategoryDTO> Categories { get; set; } = [];

	/// <summary>
	/// Gets or sets the reference data.
	/// </summary>
	/// <value>
	/// The reference data.
	/// </value>
	public ReferenceDataDTO ReferenceData { get; set; } = new();

	/// <summary>
	/// Gets or sets the query guidelines.
	/// </summary>
	/// <value>
	/// The query guidelines.
	/// </value>
	public QueryGuidelinesDTO QueryGuidelines { get; set; } = new();

	/// <summary>
	/// Gets or sets the troubleshooting domain.
	/// </summary>
	/// <value>
	/// The troubleshooting domain.
	/// </value>
	public TroubleshootingDTO Troubleshooting { get; set; } = new();
}

/// <summary>
/// The category domain.
/// </summary>
public class CategoryDTO
{
	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	/// <value>
	/// The description.
	/// </value>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the patterns.
	/// </summary>
	/// <value>
	/// The patterns.
	/// </value>
	public List<PatternDTO> Patterns { get; set; } = [];
}

/// <summary>
/// The Pattern Domain class.
/// </summary>
public class PatternDTO
{
	/// <summary>
	/// Gets or sets the name of the pattern.
	/// </summary>
	/// <value>
	/// The name of the pattern.
	/// </value>
	public string PatternName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	/// <value>
	/// The description.
	/// </value>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the sample query.
	/// </summary>
	/// <value>
	/// The sample query.
	/// </value>
	public string SampleQuery { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the parameters.
	/// </summary>
	/// <value>
	/// The parameters.
	/// </value>
	public List<string> Parameters { get; set; } = [];

	/// <summary>
	/// Gets or sets the reference values.
	/// </summary>
	/// <value>
	/// The reference values.
	/// </value>
	public List<string> ReferenceValues { get; set; } = [];

	/// <summary>
	/// Gets or sets the use cases.
	/// </summary>
	/// <value>
	/// The use cases.
	/// </value>
	public List<string> UseCases { get; set; } = [];
}

/// <summary>
/// The Reference Data Domain class.
/// </summary>
public class ReferenceDataDTO
{
	/// <summary>
	/// Gets or sets the membership statuses.
	/// </summary>
	/// <value>
	/// The membership statuses.
	/// </value>
	public List<string> MembershipStatuses { get; set; } = [];

	/// <summary>
	/// Gets or sets the payment statuses.
	/// </summary>
	/// <value>
	/// The payment statuses.
	/// </value>
	public List<string> PaymentStatuses { get; set; } = [];

	/// <summary>
	/// Gets or sets the duration types.
	/// </summary>
	/// <value>
	/// The duration types.
	/// </value>
	public List<string> DurationTypes { get; set; } = [];

	/// <summary>
	/// Gets or sets the bug severities.
	/// </summary>
	/// <value>
	/// The bug severities.
	/// </value>
	public List<string> BugSeverities { get; set; } = [];

	/// <summary>
	/// Gets or sets the bug statuses.
	/// </summary>
	/// <value>
	/// The bug statuses.
	/// </value>
	public List<string> BugStatuses { get; set; } = [];

	/// <summary>
	/// Gets or sets the ai service statuses.
	/// </summary>
	/// <value>
	/// The ai service statuses.
	/// </value>
	public List<string> AiServiceStatuses { get; set; } = [];

	/// <summary>
	/// Gets or sets the fee amounts.
	/// </summary>
	/// <value>
	/// The fee amounts.
	/// </value>
	public Dictionary<string, int> FeeAmounts { get; set; } = [];
}

/// <summary>
/// The query guidelines dto.
/// </summary>
public class QueryGuidelinesDTO
{
	/// <summary>
	/// Gets or sets the performance tips.
	/// </summary>
	/// <value>
	/// The performance tips.
	/// </value>
	public List<string> PerformanceTips { get; set; } = [];

	/// <summary>
	/// Gets or sets the security considerations.
	/// </summary>
	/// <value>
	/// The security considerations.
	/// </value>
	public List<string> SecurityConsiderations { get; set; } = [];

	/// <summary>
	/// Gets or sets the common filters.
	/// </summary>
	/// <value>
	/// The common filters.
	/// </value>
	public List<string> CommonFilters { get; set; } = [];
}

/// <summary>
/// The troubleshooting domain.
/// </summary>
public class TroubleshootingDTO
{
	/// <summary>
	/// Gets or sets the common issues.
	/// </summary>
	/// <value>
	/// The common issues.
	/// </value>
	public List<CommonIssueDTO> CommonIssues { get; set; } = [];
}

/// <summary>
/// The Common Issue domain.
/// </summary>
public class CommonIssueDTO
{
	/// <summary>
	/// Gets or sets the issue.
	/// </summary>
	/// <value>
	/// The issue.
	/// </value>
	public string Issue { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the solution.
	/// </summary>
	/// <value>
	/// The solution.
	/// </value>
	public string Solution { get; set; } = string.Empty;
}
