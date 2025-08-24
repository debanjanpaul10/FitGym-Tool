// *********************************************************************************
//	<copyright file="IAIServicesManager.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The AI services manager interface.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities.AIEntities;

namespace FitGymTool.Domain.DrivenPorts;

/// <summary>
/// The AI services manager interface.
/// </summary>
public interface IAIServicesManager
{
	/// <summary>
	/// Gets the bug severity from ai service asynchronous.
	/// </summary>
	/// <param name="bugSeverityInput">The bug severity input.</param>
	/// <returns>The bug severity response.</returns>
	Task<BugSeverityResponse> GetBugSeverityFromAIServiceAsync(BugSeverityInput bugSeverityInput);

	/// <summary>
	/// Gets the chatbot response asynchronous.
	/// </summary>
	/// <param name="userQueryRequest">The user query request.</param>
	/// <returns>The ai agent response.</returns>
	Task<AIChatbotResponse> GetChatbotResponseAsync(UserQueryRequest userQueryRequest);

	/// <summary>
	/// Gets the SQL query markdown response asynchronous.
	/// </summary>
	/// <param name="sqlQueryResult">The SQL query result.</param>
	/// <returns>The sql markdown response.</returns>
	Task<string> GetSQLQueryMarkdownResponseAsync(SqlQueryResult sqlQueryResult);

	/// <summary>
	/// Gets the list of followup questions.
	/// </summary>
	/// <param name="followupQuestionsRequestDomain">The followup questions request.</param>
	/// <returns>The list of followup questions.</returns>
	Task<IEnumerable<string>> GetFollowupQuestionsResponseAsync(FollowupQuestionsRequestDomain followupQuestionsRequestDomain);
}
