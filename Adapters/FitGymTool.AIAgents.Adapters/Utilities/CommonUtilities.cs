using FitGymTool.Domain.DomainEntities.AIEntities;
using Newtonsoft.Json;
using static FitGymTool.AIAgents.Adapters.Helpers.Constants;

namespace FitGymTool.AIAgents.Adapters.Utilities;

/// <summary>
/// The common utilities class.
/// </summary>
public static class CommonUtilities
{
	/// <summary>
	/// Prepares the agent string response.
	/// </summary>
	/// <param name="responseString">The response string.</param>
	/// <returns>The prepared AI response.</returns>
	/// <exception cref="System.Exception"></exception>
	public static string PrepareAgentStringResponse(string responseString)
	{
		var aiResponse = JsonConvert.DeserializeObject<AIAgentResponse>(responseString) ?? new AIAgentResponse();
		return aiResponse.ResponseData.ToString() ?? throw new Exception(ExceptionConstants.AiServicesCannotBeAvailedExceptionConstant);
	}
}
