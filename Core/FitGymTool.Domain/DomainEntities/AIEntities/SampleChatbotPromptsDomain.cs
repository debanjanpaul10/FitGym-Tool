// *********************************************************************************
//	<copyright file="SampleChatbotPromptsDomain.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>Sample Chatbot Prompts Domain model.</summary>
// *********************************************************************************
namespace FitGymTool.Domain.DomainEntities.AIEntities;

/// <summary>
/// Sample Chatbot Prompts domain model.
/// </summary>
/// <seealso cref="BaseEntity"/>
public class SampleChatbotPromptsDomain : BaseEntity
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the area.
	/// </summary>
	/// <value>
	/// The area.
	/// </value>
	public string Area { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the name of the prompt.
	/// </summary>
	/// <value>
	/// The name of the prompt.
	/// </value>
	public string PromptName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the prompt description.
	/// </summary>
	/// <value>
	/// The prompt description.
	/// </value>
	public string PromptDescription { get; set; } = string.Empty;
}
