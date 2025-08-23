// *********************************************************************************
//	<copyright file="AIServicesHandler.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The AI Services Handler.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Adapters.Models.Response.MetadataEntities;
using FitGymTool.Domain.DomainEntities.AIEntities;
using FitGymTool.Domain.DrivingPorts;

namespace FitGymTool.API.Adapters.Handlers;

/// <summary>
/// The AI Services Handler.
/// </summary>
/// <param name="aiServices">The AI services.</param>
/// <param name="mapper">The auto mapper.</param>
/// <seealso cref="FitGymTool.API.Adapters.Contracts.IAIServicesHandler" />
public class AIServicesHandler(IMapper mapper, IAiServices aiServices) : IAIServicesHandler
{
	/// <summary>
	/// Gets the active ai features asynchronous.
	/// </summary>
	/// <returns>
	/// The list of <see cref="AIFeaturesDTO" />
	/// </returns>
	public async Task<IEnumerable<AIFeaturesDTO>> GetActiveAIFeaturesAsync()
	{
		var domainAiFeaturesData = await aiServices.GetActiveAIFeaturesAsync().ConfigureAwait(false);
		return mapper.Map<IEnumerable<AIFeaturesDTO>>(domainAiFeaturesData);
	}

	/// <summary>
	/// Gets the bug severity from ai service asynchronous.
	/// </summary>
	/// <param name="bugSeverityInput">The bug severity input.</param>
	/// <returns>
	/// The bug severity response.
	/// </returns>
	public async Task<BugSeverityResponseDTO> GetBugSeverityFromAIServiceAsync(BugSeverityInputDTO bugSeverityInput)
	{
		var domainInputData = mapper.Map<BugSeverityInput>(bugSeverityInput);
		var domainResponse = await aiServices.GetBugSeverityFromAIServiceAsync(domainInputData).ConfigureAwait(false);
		return mapper.Map<BugSeverityResponseDTO>(domainResponse);
	}

	/// <summary>
	/// Gets the chatbot response asynchronous.
	/// </summary>
	/// <param name="userQueryRequest">The user query request.</param>
	/// <returns>
	/// The ai agent response.
	/// </returns>
	public async Task<AIChatbotResponseDTO> GetChatbotResponseAsync(ChatMessageRequestDTO userQueryRequest)
	{
		var domainInput = mapper.Map<UserQueryRequest>(userQueryRequest);
		var domainResponse = await aiServices.GetChatbotResponseAsync(domainInput).ConfigureAwait(false);
		return mapper.Map<AIChatbotResponseDTO>(domainResponse);
	}

	/// <summary>
	/// Gets the database knowledge pieces json asynchronous.
	/// </summary>
	/// <returns>
	/// The database knowledge base DTO.
	/// </returns>
	public async Task<DatabaseKnowledgeBaseDTO> GetDatabaseKnowledgePiecesJsonAsync()
	{
		var domainResponse = await aiServices.GetDatabaseKnowledgePiecesJsonAsync().ConfigureAwait(false);
		return mapper.Map<DatabaseKnowledgeBaseDTO>(domainResponse);
	}

	/// <summary>
	/// Gets the database schema json asynchronous.
	/// </summary>
	/// <returns>
	/// The database schema domain.
	/// </returns>
	public async Task<DatabaseSchemaDTO> GetDatabaseSchemaJsonAsync()
	{
		var domainResponse = await aiServices.GetDatabaseSchemaJsonAsync().ConfigureAwait(false);
		return mapper.Map<DatabaseSchemaDTO>(domainResponse);
	}

	/// <summary>
	/// Gets the sample prompts for chatbot asynchronous.
	/// </summary>
	/// <returns>
	/// The list of <see cref="SampleChatbotPromptsDTO" />
	/// </returns>
	public async Task<IEnumerable<SampleChatbotPromptsDTO>> GetSamplePromptsForChatbotAsync()
	{
		var domainResponse = await aiServices.GetSamplePromptsForChatbotAsync().ConfigureAwait(false);
		return mapper.Map<IEnumerable<SampleChatbotPromptsDTO>>(domainResponse);
	}
}
