// *********************************************************************************
//	<copyright file="IAiServices.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The AI Services Interface.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities.AIEntities;
using FitGymTool.Domain.DomainEntities.MetadataEntities;

namespace FitGymTool.Domain.DrivingPorts;

/// <summary>
/// The AI Services Interface.
/// </summary>
public interface IAiServices
{
	/// <summary>
	/// Gets the bug severity from ai service asynchronous.
	/// </summary>
	/// <param name="bugSeverityInput">The bug severity input.</param>
	/// <returns>The bug severity response.</returns>
	Task<BugSeverityResponse> GetBugSeverityFromAIServiceAsync(BugSeverityInput bugSeverityInput);

	/// <summary>
	/// Gets the active ai features asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="AIFeature"/></returns>
	Task<IEnumerable<AIFeature>> GetActiveAIFeaturesAsync();

	/// <summary>
	/// Gets the chatbot response asynchronous.
	/// </summary>
	/// <param name="userQueryRequest">The user query request.</param>
	/// <returns>The ai agent response.</returns>
	Task<AIChatbotResponse> GetChatbotResponseAsync(UserQueryRequest userQueryRequest);

	/// <summary>
	/// Gets the database schema json asynchronous.
	/// </summary>
	/// <returns>The database schema domain.</returns>
	Task<DatabaseSchemaDomain> GetDatabaseSchemaJsonAsync();

	/// <summary>
	/// Gets the database knowledge pieces json asynchronous.
	/// </summary>
	/// <returns>
	/// The database knowledge base domain.
	/// </returns>
	Task<DatabaseKnowledgeBaseDomain> GetDatabaseKnowledgePiecesJsonAsync();
}
