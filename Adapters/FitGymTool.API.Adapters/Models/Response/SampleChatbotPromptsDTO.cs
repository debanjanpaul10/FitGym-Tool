// *********************************************************************************
//	<copyright file="SampleChatbotPromptsDTO.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>Sample Chatbot Prompts DTO.</summary>
// *********************************************************************************

namespace FitGymTool.API.Adapters.Models.Response;

/// <summary>
/// Sample Chatbot Prompts DTO.
/// </summary>
public class SampleChatbotPromptsDTO
{
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
	public string PromptDescription { get; set;} = string.Empty;
}
