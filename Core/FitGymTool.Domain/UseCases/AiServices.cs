// *********************************************************************************
//	<copyright file="AiServices.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The AI Services Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities.AIEntities;
using FitGymTool.Domain.DrivenPorts;
using FitGymTool.Domain.DrivingPorts;
using FitGymTool.Domain.Helpers;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Newtonsoft.Json;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Domain.UseCases;

/// <summary>
/// The AI Services Class.
/// </summary>
/// <param name="aiServicesManager">The AI services manager.</param>
/// <param name="commonDataManager">The common data manager.</param>
/// <param name="logger">The logger service.</param>
/// <param name="mongoDbDatabaseManager">The mongo db database manager.</param>
/// <seealso cref="FitGymTool.Domain.DrivingPorts.IAiServices" />
public class AiServices(ILogger<AiServices> logger, IAIServicesManager aiServicesManager, ICommonDataManager commonDataManager, IMongoDbDatabaseManager mongoDbDatabaseManager) : IAiServices
{
	/// <summary>
	/// Gets the active ai features asynchronous.
	/// </summary>
	/// <returns>
	/// The list of <see cref="AIFeature" />
	/// </returns>
	public async Task<IEnumerable<AIFeature>> GetActiveAIFeaturesAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetActiveAIFeaturesAsync), DateTime.UtcNow, string.Empty));
			return await commonDataManager.GetActiveAIFeaturesAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetActiveAIFeaturesAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetActiveAIFeaturesAsync), DateTime.UtcNow, string.Empty));
		}
	}

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
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetBugSeverityFromAIServiceAsync), DateTime.UtcNow, bugSeverityInput.BugTitle));
			return await aiServicesManager.GetBugSeverityFromAIServiceAsync(bugSeverityInput).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetBugSeverityFromAIServiceAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetBugSeverityFromAIServiceAsync), DateTime.UtcNow, bugSeverityInput.BugTitle));
		}
	}

	/// <summary>
	/// Gets the chatbot response asynchronous.
	/// </summary>
	/// <param name="userQueryRequest">The user query request.</param>
	/// <param name="areFollowupQuestionsEnabled">The boolean for followup questions status</param>
	/// <returns>
	/// The ai agent response.
	/// </returns>
	public async Task<AIChatbotResponse> GetChatbotResponseAsync(UserQueryRequest userQueryRequest, bool areFollowupQuestionsEnabled)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetChatbotResponseAsync), DateTime.UtcNow, userQueryRequest.UserQuery));

			var aiChatbotResponse = new AIChatbotResponse();
			var userIntent = await aiServicesManager.DetectUserIntentAsync(userQueryRequest).ConfigureAwait(false);
			if (string.IsNullOrEmpty(userIntent))
			{
				throw new Exception(ExceptionConstants.SomethingWentWrongMessage);
			}

			var normalizedIntent = userIntent.Trim().ToUpperInvariant();
			var aiResponse = normalizedIntent switch
			{
				IntentConstants.GreetingIntent => await aiServicesManager.HandleUserGreetingIntentAsync().ConfigureAwait(false),
				IntentConstants.SQLIntent => await InvokeSqlFunctionAsync(userQueryRequest.UserQuery, aiChatbotResponse).ConfigureAwait(false),
				IntentConstants.RAGIntent => await InvokeRAGFunctionAsync(userQueryRequest.UserQuery).ConfigureAwait(false),
				IntentConstants.UnclearIntent => "Cannot determine the user intent",
				_ => string.Empty
			};

			aiChatbotResponse.PrepareAgentChatbotReponse(userIntent.Trim(), userQueryRequest.UserQuery, aiResponse);
			if (areFollowupQuestionsEnabled && (normalizedIntent != IntentConstants.GreetingIntent && normalizedIntent != IntentConstants.UnclearIntent))
			{
				await HandleFollowupQuestionsDataAsync(aiChatbotResponse).ConfigureAwait(false);
			}

			return aiChatbotResponse;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetChatbotResponseAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetChatbotResponseAsync), DateTime.UtcNow, userQueryRequest.UserQuery));
		}
	}

	/// <summary>
	/// Gets the sample prompts for chatbot asynchronous.
	/// </summary>
	/// <returns>
	/// The list of <see cref="SampleChatbotPromptsDomain" />
	/// </returns>
	public async Task<IEnumerable<SampleChatbotPromptsDomain>> GetSamplePromptsForChatbotAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, string.Empty));
			return await commonDataManager.GetSamplePromptsForChatbotAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, string.Empty));
		}
	}

	#region PRIVATE METHODS

	/// <summary>
	/// Invokes the SQL function asynchronous.
	/// </summary>
	/// <param name="userInput">The user input.</param>
	/// <param name="aiChatbotResponse">The ai chatbot response.</param>
	/// <returns>The AI response data.</returns>
	private async Task<string> InvokeSqlFunctionAsync(string userInput, AIChatbotResponse aiChatbotResponse)
	{
		var databaseSchemaTask = mongoDbDatabaseManager.GetDatabaseSchemaJsonAsync();
		var databaseKnowledgeBaseTask = mongoDbDatabaseManager.GetDatabaseKnowledgePiecesJsonAsync();
		await Task.WhenAll(databaseSchemaTask, databaseKnowledgeBaseTask).ConfigureAwait(false);

		var nltosqlInput = new NltosqlInputDomain()
		{
			DatabaseSchema = JsonConvert.SerializeObject(databaseSchemaTask.Result),
			KnowledgeBase = JsonConvert.SerializeObject(databaseKnowledgeBaseTask.Result),
			Source = ConfigurationConstants.SourceName,
			UserQuery = userInput
		};

		var sqlQuery = await aiServicesManager.HandleNLToSQLResponseAsync(nltosqlInput).ConfigureAwait(false);
		var trimmedQuery = sqlQuery.Replace("```sql", string.Empty).Replace("```", string.Empty).Replace("\n", string.Empty).Trim();
		var jsonQuery = await commonDataManager.ExecuteAISQLQueryAsync(trimmedQuery).ConfigureAwait(false);

		var sqlQueryResult = new SqlQueryResult() { JsonQuery = jsonQuery };
		aiChatbotResponse.SqlQuery = trimmedQuery;
		return await aiServicesManager.GetSQLQueryMarkdownResponseAsync(sqlQueryResult).ConfigureAwait(false);
	}

	/// <summary>
	/// Invokes the rag function asynchronous.
	/// </summary>
	/// <param name="userInput">The user input.</param>
	/// <returns>The AI response data.</returns>
	private async Task<string> InvokeRAGFunctionAsync(string userInput)
	{
		var knowledgeBase = await mongoDbDatabaseManager.GetDatabaseKnowledgePiecesJsonAsync().ConfigureAwait(false);
		var skillsInput = new SkillsInputDomain()
		{
			KnowledgeBase = JsonConvert.SerializeObject(knowledgeBase),
			Source = ConfigurationConstants.SourceName,
			UserQuery = userInput
		};

		return await aiServicesManager.HandleRAGTextResponseAsync(skillsInput).ConfigureAwait(false);
	}

	/// <summary>
	/// Handles the followup questions data async.
	/// </summary>
	/// <param name="aiResult">The ai result.</param>
	/// <returns>A task to wait on.</returns>
	private async Task HandleFollowupQuestionsDataAsync(AIChatbotResponse aiResult)
	{
		var followupQuestionsDataDomain = new FollowupQuestionsRequestDomain
		{
			AiResponseData = aiResult.UserIntent == IntentConstants.RAGIntent ? aiResult.AIResponseData.Replace("`", "'") : aiResult.AIResponseData,
			UserIntent = aiResult.UserIntent,
			UserQuery = aiResult.UserQuery
		};

		aiResult.FollowupQuestions = await aiServicesManager.GetFollowupQuestionsResponseAsync(followupQuestionsDataDomain).ConfigureAwait(false);
	}

	#endregion
}
