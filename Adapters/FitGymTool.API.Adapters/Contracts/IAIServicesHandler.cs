// *********************************************************************************
//	<copyright file="IAIServicesHandler.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The AI Services Handler interface.</summary>
// *********************************************************************************

using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Adapters.Models.Response.MetadataEntities;

namespace FitGymTool.API.Adapters.Contracts;

/// <summary>
/// The AI Services Handler interface.
/// </summary>
public interface IAIServicesHandler
{
	/// <summary>
	/// Gets the bug severity from ai service asynchronous.
	/// </summary>
	/// <param name="bugSeverityInput">The bug severity input.</param>
	/// <returns>The bug severity response.</returns>
	Task<BugSeverityResponseDTO> GetBugSeverityFromAIServiceAsync(BugSeverityInputDTO bugSeverityInput);

	/// <summary>
	/// Gets the active ai features asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="AIFeaturesDTO"/></returns>
	Task<IEnumerable<AIFeaturesDTO>> GetActiveAIFeaturesAsync();

	/// <summary>
	/// Gets the chatbot response asynchronous.
	/// </summary>
	/// <param name="chatMessageRequest">The user query request.</param>
	/// <returns>The ai agent response.</returns>
	Task<AIChatbotResponseDTO> GetChatbotResponseAsync(ChatMessageRequestDTO chatMessageRequest);

	/// <summary>
	/// Gets the database schema json asynchronous.
	/// </summary>
	/// <returns>The database schema domain.</returns>
	Task<DatabaseSchemaDTO> GetDatabaseSchemaJsonAsync();

	/// <summary>
	/// Gets the database knowledge pieces json asynchronous.
	/// </summary>
	/// <returns>
	/// The database knowledge base DTO.
	/// </returns>
	Task<DatabaseKnowledgeBaseDTO> GetDatabaseKnowledgePiecesJsonAsync();

	/// <summary>
	/// Gets the sample prompts for chatbot asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="SampleChatbotPromptsDTO"/></returns>
	Task<IEnumerable<SampleChatbotPromptsDTO>> GetSamplePromptsForChatbotAsync();
}
