using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitGymTool.Domain.DomainEntities.MetadataEntities;

/// <summary>
/// The DB knowledge base domain model.
/// </summary>
[BsonIgnoreExtraElements]
public class DatabaseKnowledgeBaseDomain
{
	/// <summary>
	/// The Id.
	/// </summary>
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the name of the database.
	/// </summary>
	/// <value>
	/// The name of the database.
	/// </value>
	[BsonElement("database_name")]
	public string DatabaseName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	/// <value>
	/// The description.
	/// </value>
	[BsonElement("description")]
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the version.
	/// </summary>
	/// <value>
	/// The version.
	/// </value>
	[BsonElement("version")]
	public string Version { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the last updated.
	/// </summary>
	/// <value>
	/// The last updated.
	/// </value>
	[BsonElement("last_updated")]
	public string LastUpdated { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the categories.
	/// </summary>
	/// <value>
	/// The categories.
	/// </value>
	[BsonElement("categories")]
	public Dictionary<string, CategoryDomain> Categories { get; set; } = [];

	/// <summary>
	/// Gets or sets the reference data.
	/// </summary>
	/// <value>
	/// The reference data.
	/// </value>
	[BsonElement("reference_data")]
	public ReferenceDataDomain ReferenceData { get; set; } = new();

	/// <summary>
	/// Gets or sets the query guidelines.
	/// </summary>
	/// <value>
	/// The query guidelines.
	/// </value>
	[BsonElement("query_guidelines")]
	public QueryGuidelinesDomain QueryGuidelines { get; set; } = new();

	/// <summary>
	/// Gets or sets the troubleshooting domain.
	/// </summary>
	/// <value>
	/// The troubleshooting domain.
	/// </value>
	[BsonElement("troubleshooting")]
	public TroubleshootingDomain Troubleshooting { get; set; } = new();
}

/// <summary>
/// The category domain.
/// </summary>
public class CategoryDomain
{
	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	/// <value>
	/// The description.
	/// </value>
	[BsonElement("description")]
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the patterns.
	/// </summary>
	/// <value>
	/// The patterns.
	/// </value>
	[BsonElement("patterns")]
	public List<PatternDomain> Patterns { get; set; } = [];
}

/// <summary>
/// The Pattern Domain class.
/// </summary>
public class PatternDomain
{
	/// <summary>
	/// Gets or sets the name of the pattern.
	/// </summary>
	/// <value>
	/// The name of the pattern.
	/// </value>
	[BsonElement("pattern_name")]
	public string PatternName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	/// <value>
	/// The description.
	/// </value>
	[BsonElement("description")]
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the sample query.
	/// </summary>
	/// <value>
	/// The sample query.
	/// </value>
	[BsonElement("sample_query")]
	public string SampleQuery { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the parameters.
	/// </summary>
	/// <value>
	/// The parameters.
	/// </value>
	[BsonElement("parameters")]
	public List<string> Parameters { get; set; } = [];

	/// <summary>
	/// Gets or sets the reference values.
	/// </summary>
	/// <value>
	/// The reference values.
	/// </value>
	[BsonElement("reference_values")]
	public List<string> ReferenceValues { get; set; } = [];

	/// <summary>
	/// Gets or sets the use cases.
	/// </summary>
	/// <value>
	/// The use cases.
	/// </value>
	[BsonElement("use_cases")]
	public List<string> UseCases { get; set; } = [];
}

/// <summary>
/// The Reference Data Domain class.
/// </summary>
public class ReferenceDataDomain
{
	/// <summary>
	/// Gets or sets the membership statuses.
	/// </summary>
	/// <value>
	/// The membership statuses.
	/// </value>
	[BsonElement("membership_statuses")]
	public List<string> MembershipStatuses { get; set; } = [];

	/// <summary>
	/// Gets or sets the payment statuses.
	/// </summary>
	/// <value>
	/// The payment statuses.
	/// </value>
	[BsonElement("payment_statuses")]
	public List<string> PaymentStatuses { get; set; } = [];

	/// <summary>
	/// Gets or sets the duration types.
	/// </summary>
	/// <value>
	/// The duration types.
	/// </value>
	[BsonElement("duration_types")]
	public List<string> DurationTypes { get; set; } = [];

	/// <summary>
	/// Gets or sets the bug severities.
	/// </summary>
	/// <value>
	/// The bug severities.
	/// </value>
	[BsonElement("bug_severities")]
	public List<string> BugSeverities { get; set; } = [];

	/// <summary>
	/// Gets or sets the bug statuses.
	/// </summary>
	/// <value>
	/// The bug statuses.
	/// </value>
	[BsonElement("bug_statuses")]
	public List<string> BugStatuses { get; set; } = [];

	/// <summary>
	/// Gets or sets the ai service statuses.
	/// </summary>
	/// <value>
	/// The ai service statuses.
	/// </value>
	[BsonElement("ai_service_statuses")]
	public List<string> AiServiceStatuses { get; set; } = [];

	/// <summary>
	/// Gets or sets the fee amounts.
	/// </summary>
	/// <value>
	/// The fee amounts.
	/// </value>
	[BsonElement("fee_amounts")]
	public Dictionary<string, int> FeeAmounts { get; set; } = [];
}

/// <summary>
/// The query guidelines domain.
/// </summary>
public class QueryGuidelinesDomain
{
	/// <summary>
	/// Gets or sets the performance tips.
	/// </summary>
	/// <value>
	/// The performance tips.
	/// </value>
	[BsonElement("performance_tips")]
	public List<string> PerformanceTips { get; set; } = [];

	/// <summary>
	/// Gets or sets the security considerations.
	/// </summary>
	/// <value>
	/// The security considerations.
	/// </value>
	[BsonElement("security_considerations")]
	public List<string> SecurityConsiderations { get; set; } = [];

	/// <summary>
	/// Gets or sets the common filters.
	/// </summary>
	/// <value>
	/// The common filters.
	/// </value>
	[BsonElement("common_filters")]
	public List<string> CommonFilters { get; set; } = [];
}

/// <summary>
/// The troubleshooting domain.
/// </summary>
public class TroubleshootingDomain
{
	/// <summary>
	/// Gets or sets the common issues.
	/// </summary>
	/// <value>
	/// The common issues.
	/// </value>
	[BsonElement("common_issues")]
	public List<CommonIssueDomain> CommonIssues { get; set; } = [];
}

/// <summary>
/// The Common Issue domain.
/// </summary>
public class CommonIssueDomain
{
	/// <summary>
	/// Gets or sets the issue.
	/// </summary>
	/// <value>
	/// The issue.
	/// </value>
	[BsonElement("issue")]
	public string Issue { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the solution.
	/// </summary>
	/// <value>
	/// The solution.
	/// </value>
	[BsonElement("solution")]
	public string Solution { get; set; } = string.Empty;
}
