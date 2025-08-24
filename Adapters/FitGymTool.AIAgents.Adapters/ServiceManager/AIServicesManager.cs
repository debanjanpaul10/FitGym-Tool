// *********************************************************************************
//	<copyright file="AIServicesManager.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The AI services manager class.</summary>
// *********************************************************************************

using FitGymTool.AIAgents.Adapters.Utilities;
using FitGymTool.Domain.DomainEntities.AIEntities;
using FitGymTool.Domain.DrivenPorts;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.Json;
using static FitGymTool.AIAgents.Adapters.Helpers.Constants;

namespace FitGymTool.AIAgents.Adapters.ServiceManager;

/// <summary>
/// The AI Services Manager.
/// </summary>
/// <param name="httpClientHelper">The http client helper.</param>
/// <param name="logger">The logger service.</param>
/// <seealso cref="FitGymTool.Domain.DrivenPorts.IAIServicesManager" />
public class AIServicesManager(IHttpClientHelper httpClientHelper, ILogger<AIServicesManager> logger) : IAIServicesManager
{
	/// <summary>
	/// Gets the bug severity from ai service asynchronous.
	/// </summary>
	/// <param name="bugSeverityInput">The bug severity input.</param>
	/// <returns>
	/// The bug severity response.
	/// </returns>
	public async Task<BugSeverityResponse> GetBugSeverityFromAIServiceAsync(BugSeverityInput bugSeverityInput)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(GetBugSeverityFromAIServiceAsync), DateTime.UtcNow, bugSeverityInput.BugTitle));
			var response = await httpClientHelper.GetAIResponseAsync(bugSeverityInput, AIAgentsRoutesConstants.GetBugSeverity_ApiRoute).ConfigureAwait(false);
			return JsonSerializer.Deserialize<BugSeverityResponse>(await response.Content.ReadAsStringAsync()) ?? new BugSeverityResponse();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(GetBugSeverityFromAIServiceAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(GetBugSeverityFromAIServiceAsync), DateTime.UtcNow, bugSeverityInput.BugTitle));
		}
	}

	/// <summary>
	/// Gets the chatbot response asynchronous.
	/// </summary>
	/// <param name="userQueryRequest">The user query request.</param>
	/// <returns>
	/// The ai agent response.
	/// </returns>
	public async Task<AIChatbotResponse> GetChatbotResponseAsync(UserQueryRequest userQueryRequest)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(GetChatbotResponseAsync), DateTime.UtcNow, userQueryRequest.UserQuery));
			var response = await httpClientHelper.GetAIResponseAsync(userQueryRequest, AIAgentsRoutesConstants.GetChatbotResponse_ApiRoute).ConfigureAwait(false);
			var responseString = await response.Content.ReadAsStringAsync();

			var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var aiResponse = JsonSerializer.Deserialize<AIAgentResponse>(responseString, jsonSerializerOptions) ?? new AIAgentResponse();
			var responseDataJson = JsonSerializer.Serialize(aiResponse.ResponseData);
			return JsonSerializer.Deserialize<AIChatbotResponse>(responseDataJson, jsonSerializerOptions) ?? new AIChatbotResponse();

		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(GetChatbotResponseAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(GetChatbotResponseAsync), DateTime.UtcNow, userQueryRequest.UserQuery));
		}
	}

	/// <summary>
	/// Gets the list of followup questions.
	/// </summary>
	/// <param name="followupQuestionsRequestDomain">The followup questions request.</param>
	/// <returns>The list of followup questions.</returns>
	public async Task<IEnumerable<string>> GetFollowupQuestionsResponseAsync(FollowupQuestionsRequestDomain followupQuestionsRequestDomain)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(GetFollowupQuestionsResponseAsync), DateTime.UtcNow, followupQuestionsRequestDomain.UserQuery));
			var response = await httpClientHelper.GetAIResponseAsync(followupQuestionsRequestDomain, AIAgentsRoutesConstants.GetFollowupQuestionsResponse_ApiRoute).ConfigureAwait(false);
			var responseString = await response.Content.ReadAsStringAsync();

			var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var aiResponse = JsonSerializer.Deserialize<AIAgentResponse>(responseString, jsonSerializerOptions) ?? new AIAgentResponse();
			return JsonSerializer.Deserialize<IEnumerable<string>>(JsonSerializer.Serialize(aiResponse.ResponseData)) ?? [];
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(GetFollowupQuestionsResponseAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(GetFollowupQuestionsResponseAsync), DateTime.UtcNow, followupQuestionsRequestDomain.UserQuery));
		}
	}

	/// <summary>
	/// Gets the SQL query markdown response asynchronous.
	/// </summary>
	/// <param name="sqlQueryResult">The SQL query result.</param>
	/// <returns>The sql markdown result.</returns>
	/// <exception cref="System.Exception"></exception>
	public async Task<string> GetSQLQueryMarkdownResponseAsync(SqlQueryResult sqlQueryResult)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(GetSQLQueryMarkdownResponseAsync), DateTime.UtcNow, string.Empty));
			var response = await httpClientHelper.GetAIResponseAsync(sqlQueryResult, AIAgentsRoutesConstants.GetSQLQueryMarkdownResponse_ApiRoute).ConfigureAwait(false);
			var responseString = await response.Content.ReadAsStringAsync();

			var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var aiResponse = JsonSerializer.Deserialize<AIAgentResponse>(responseString, jsonSerializerOptions) ?? new AIAgentResponse();
			return aiResponse.ResponseData.ToString() ?? throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(GetSQLQueryMarkdownResponseAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(GetSQLQueryMarkdownResponseAsync), DateTime.UtcNow, string.Empty));
		}
	}
}
