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
	/// Detects the user intent asynchronous.
	/// </summary>
	/// <param name="userQueryRequest">The user query request.</param>
	/// <returns>The intent string.</returns>
	Task<string> DetectUserIntentAsync(UserQueryRequest userQueryRequest);

	/// <summary>
	/// Handles the user greeting intent asynchronous.
	/// </summary>
	/// <returns>The greeting from ai agent.</returns>
	Task<string> HandleUserGreetingIntentAsync();

	/// <summary>
	/// Handles the rag text response asynchronous.
	/// </summary>
	/// <param name="skillsInputDomain">The skills input domain.</param>
	/// <returns>The ai generated response.</returns>
	Task<string> HandleRAGTextResponseAsync(SkillsInputDomain skillsInputDomain);

	/// <summary>
	/// Handles the nl to SQL response asynchronous.
	/// </summary>
	/// <param name="nltosqlInput">The nltosql input.</param>
	/// <returns>The ai generated response.</returns>
	Task<string> HandleNLToSQLResponseAsync(NltosqlInputDomain nltosqlInput);

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
