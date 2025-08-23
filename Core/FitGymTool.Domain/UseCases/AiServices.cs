// *********************************************************************************
//	<copyright file="AiServices.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The AI Services Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities.AIEntities;
using FitGymTool.Domain.DomainEntities.MetadataEntities;
using FitGymTool.Domain.DrivenPorts;
using FitGymTool.Domain.DrivingPorts;
using Microsoft.Extensions.Logging;
using System.Globalization;
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
	/// <returns>
	/// The ai agent response.
	/// </returns>
	public async Task<AIChatbotResponse> GetChatbotResponseAsync(UserQueryRequest userQueryRequest)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetChatbotResponseAsync), DateTime.UtcNow, userQueryRequest.UserQuery));
			var aiResult = await aiServicesManager.GetChatbotResponseAsync(userQueryRequest).ConfigureAwait(false);
			if (aiResult is null)
			{
				throw new Exception(ExceptionConstants.SomethingWentWrongMessage);
			}

			if (aiResult.UserIntent.Trim().Contains(HeaderConstants.SQLConstant, StringComparison.InvariantCultureIgnoreCase))
			{
				return await HandleSqlQueryResultsExecutionAsync(aiResult).ConfigureAwait(false);
			}

			return aiResult;
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
	/// Gets the database knowledge pieces json asynchronous.
	/// </summary>
	/// <returns>
	/// The database knowledge base domain.
	/// </returns>
	public async Task<DatabaseKnowledgeBaseDomain> GetDatabaseKnowledgePiecesJsonAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetDatabaseKnowledgePiecesJsonAsync), DateTime.UtcNow, string.Empty));
			return await mongoDbDatabaseManager.GetDatabaseKnowledgePiecesJsonAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetDatabaseKnowledgePiecesJsonAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetDatabaseKnowledgePiecesJsonAsync), DateTime.UtcNow, string.Empty));
		}
	}

	/// <summary>
	/// Gets the database schema json asynchronous.
	/// </summary>
	/// <returns>
	/// The database schema domain.
	/// </returns>
	public async Task<DatabaseSchemaDomain> GetDatabaseSchemaJsonAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetDatabaseSchemaJsonAsync), DateTime.UtcNow, string.Empty));
			return await mongoDbDatabaseManager.GetDatabaseSchemaJsonAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetDatabaseSchemaJsonAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetDatabaseSchemaJsonAsync), DateTime.UtcNow, string.Empty));
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
	/// Handles the SQL query results execution asynchronous.
	/// </summary>
	/// <param name="aiResult">The ai result.</param>
	/// <returns>The AI chatbot response.</returns>
	private async Task<AIChatbotResponse> HandleSqlQueryResultsExecutionAsync(AIChatbotResponse aiResult)
	{
		var cleanedSqlQuery = aiResult.AIResponseData.Replace("```sql", string.Empty).Replace("```", string.Empty).Replace("\n", string.Empty).Trim();
		aiResult.SqlQuery = cleanedSqlQuery;

		var jsonQuery = await commonDataManager.ExecuteAISQLQueryAsync(cleanedSqlQuery).ConfigureAwait(false);
		var markdownResponse = await aiServicesManager.GetSQLQueryMarkdownResponseAsync(new SqlQueryResult() { JsonQuery = jsonQuery }).ConfigureAwait(false);

		aiResult.AIResponseData = markdownResponse;
		return aiResult;
	}

	#endregion
}
