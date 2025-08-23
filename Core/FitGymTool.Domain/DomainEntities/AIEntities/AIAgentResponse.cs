// *********************************************************************************
//	<copyright file="AIAgentResponse.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The AI Agent Response.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.DomainEntities.AIEntities;

/// <summary>
/// The AI Agent Response.
/// </summary>
public class AIAgentResponse
{
	/// <summary>
	/// Gets or sets the status code.
	/// </summary>
	/// <value>
	/// The status code.
	/// </value>
	public int StatusCode { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this instance is success.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
	/// </value>
	public bool IsSuccess { get; set; }

	/// <summary>
	/// Gets or sets the response data.
	/// </summary>
	/// <value>
	/// The response data.
	/// </value>
	public object ResponseData { get; set; } = default!;
}

/// <summary>
/// The AI Chatbot Response domain model.
/// </summary>
public class AIChatbotResponse
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
