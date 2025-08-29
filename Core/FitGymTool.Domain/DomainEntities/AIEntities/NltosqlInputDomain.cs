namespace FitGymTool.Domain.DomainEntities.AIEntities;

/// <summary>
/// The NL to SQL input domain.
/// </summary>
/// <seealso cref="FitGymTool.Domain.DomainEntities.AIEntities.SkillsInputDomain" />
public class NltosqlInputDomain : SkillsInputDomain
{
	/// <summary>
	/// Gets or sets the database schema.
	/// </summary>
	/// <value>
	/// The database schema.
	/// </value>
	public string DatabaseSchema { get; set; } = string.Empty;
}
