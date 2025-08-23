// *********************************************************************************
//	<copyright file="AIChatbotResponseDTO.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The AI Chatbot Response DTO.</summary>
// *********************************************************************************

namespace FitGymTool.API.Adapters.Models.Response;

/// <summary>
/// The AI Chatbot Response DTO.
/// </summary>
public class AIChatbotResponseDTO
{
	/// <summary>
	/// Gets or sets the ai response data.
	/// </summary>
	/// <value>
	/// The ai response data.
	/// </value>
	public string AIResponseData { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the user query.
	/// </summary>
	/// <value>
	/// The user query.
	/// </value>
	public string UserQuery { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the user intent.
	/// </summary>
	/// <value>
	/// The user intent.
	/// </value>
	public string UserIntent { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the SQL query.
	/// </summary>
	/// <value>
	/// The SQL query.
	/// </value>
	public string? SqlQuery { get; set; } = string.Empty;
}
