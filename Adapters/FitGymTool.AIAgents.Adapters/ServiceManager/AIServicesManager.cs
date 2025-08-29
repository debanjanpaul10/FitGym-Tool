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
using Newtonsoft.Json;
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
    /// Detects the user intent asynchronous.
    /// </summary>
    /// <param name="userQueryRequest">The user query request.</param>
    /// <returns>
    /// The intent string.
    /// </returns>
    public async Task<string> DetectUserIntentAsync(UserQueryRequest userQueryRequest)
    {
        try
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(DetectUserIntentAsync), DateTime.UtcNow, userQueryRequest.UserQuery));
            var response = await httpClientHelper.GetAIResponseAsync(userQueryRequest, AIAgentsRoutesConstants.DetectUserIntent_ApiRoute).ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync();
            return CommonUtilities.PrepareAgentStringResponse(responseString);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(DetectUserIntentAsync), DateTime.UtcNow, ex.Message));
            throw;
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(DetectUserIntentAsync), DateTime.UtcNow, userQueryRequest.UserQuery));
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
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(GetBugSeverityFromAIServiceAsync), DateTime.UtcNow, bugSeverityInput.BugTitle));
            var response = await httpClientHelper.GetAIResponseAsync(bugSeverityInput, AIAgentsRoutesConstants.GetBugSeverity_ApiRoute).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<BugSeverityResponse>(await response.Content.ReadAsStringAsync()) ?? new BugSeverityResponse();
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

            var aiResponse = JsonConvert.DeserializeObject<AIAgentResponse>(responseString) ?? new AIAgentResponse();
            return JsonConvert.DeserializeObject<IEnumerable<string>>(JsonConvert.SerializeObject(aiResponse.ResponseData))
                ?? throw new Exception(ExceptionConstants.AiServicesCannotBeAvailedExceptionConstant);
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
            return CommonUtilities.PrepareAgentStringResponse(responseString);
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

    /// <summary>
    /// Handles the nl to SQL response asynchronous.
    /// </summary>
    /// <param name="nltosqlInput">The nltosql input.</param>
    /// <returns>The ai generated response.</returns>
    public async Task<string> HandleNLToSQLResponseAsync(NltosqlInputDomain nltosqlInput)
    {
        try
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(HandleNLToSQLResponseAsync), DateTime.UtcNow, nltosqlInput.UserQuery));
            var response = await httpClientHelper.GetAIResponseAsync(nltosqlInput, AIAgentsRoutesConstants.GetNlToSqlResponse_ApiRoute).ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync();
            return CommonUtilities.PrepareAgentStringResponse(responseString);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(HandleNLToSQLResponseAsync), DateTime.UtcNow, ex.Message));
            throw;
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(HandleNLToSQLResponseAsync), DateTime.UtcNow, nltosqlInput.UserQuery));
        }
    }

    /// <summary>
    /// Handles the rag text response asynchronous.
    /// </summary>
    /// <param name="skillsInputDomain">The skills input domain.</param>
    /// <returns>The ai generated response.</returns>
    public async Task<string> HandleRAGTextResponseAsync(SkillsInputDomain skillsInputDomain)
    {
        try
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(HandleRAGTextResponseAsync), DateTime.UtcNow, skillsInputDomain.UserQuery));
            var response = await httpClientHelper.GetAIResponseAsync(skillsInputDomain, AIAgentsRoutesConstants.GetRAGTextResponse_ApiRoute).ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync();
            return CommonUtilities.PrepareAgentStringResponse(responseString);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(HandleRAGTextResponseAsync), DateTime.UtcNow, ex.Message));
            throw;
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(HandleRAGTextResponseAsync), DateTime.UtcNow, skillsInputDomain.UserQuery));
        }
    }

    /// <summary>
    /// Handles the user greeting intent asynchronous.
    /// </summary>
    /// <returns>The greeting from ai agent.</returns>
    public async Task<string> HandleUserGreetingIntentAsync()
    {
        try
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(HandleUserGreetingIntentAsync), DateTime.UtcNow, string.Empty));
            var response = await httpClientHelper.GetAIResponseAsync(string.Empty, AIAgentsRoutesConstants.GetUserGreetingResponse_ApiRoute).ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync();
            return CommonUtilities.PrepareAgentStringResponse(responseString);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(HandleUserGreetingIntentAsync), DateTime.UtcNow, ex.Message));
            throw;
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(HandleUserGreetingIntentAsync), DateTime.UtcNow, string.Empty));
        }
    }
}
