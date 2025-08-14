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
using Newtonsoft.Json;
using System.Globalization;
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
			var response = await httpClientHelper.GetAIResponseAsync(bugSeverityInput, AIAgentsRoutesConstants.GetBugSeverity_ApiRoute);
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
}
