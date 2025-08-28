using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static FitGymTool.API.Helpers.APIConstants;
using static FitGymTool.API.Helpers.SwaggerConstants.AIServicesController;

namespace FitGymTool.API.Controllers;

/// <summary>
/// The AI Services Controller Class.
/// </summary>
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="aiServicesHandler">The AI services handler.</param>
/// <seealso cref="FitGymTool.API.Controllers.BaseController" />
[ApiController]
[Route(RouteConstants.AIServicesApiRoutes.BaseRoute_RoutePrefix)]
public class AIServicesController(IHttpContextAccessor httpContextAccessor, IAIServicesHandler aiServicesHandler) : BaseController(httpContextAccessor)
{
	/// <summary>
	/// Responds the user query asynchronous.
	/// </summary>
	/// <param name="chatMessage">The chat message.</param>
	/// <returns>The ai response.</returns>
	[HttpPost(RouteConstants.AIServicesApiRoutes.Respond_ApiRoute)]
	[ProducesResponseType(typeof(AIChatbotResponseDTO), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(Summary = RespondAction.Summary, Description = RespondAction.Description, OperationId = RespondAction.OperationId)]
	public async Task<ResponseDTO> RespondAsync([FromBody] ChatMessageRequestDTO chatMessage)
	{
		if (IsAuthorized())
		{
			var result = await aiServicesHandler.GetChatbotResponseAsync(chatMessage).ConfigureAwait(false);
			if (result is not null)
			{
				return HandleSuccessRequestResponse(result);
			}

			return HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.SomethingWentWrongMessageConstant);
		}

		return HandleUnAuthorizedRequestResponse();
	}

	/// <summary>
	/// Gets the bug severity status asynchronous.
	/// </summary>
	/// <param name="bugSeverityInput">The bug severity input.</param>
	/// <returns>The bug severity response dto.</returns>
	[HttpPost(RouteConstants.AIServicesApiRoutes.GetBugSeverityStatus_ApiRoute)]
	[ProducesResponseType(typeof(BugSeverityResponseDTO), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(Summary = GetBugSeverityStatusAction.Summary, Description = GetBugSeverityStatusAction.Description, OperationId = GetBugSeverityStatusAction.OperationId)]
	public async Task<ResponseDTO> GetBugSeverityStatusAsync([FromBody] BugSeverityInputDTO bugSeverityInput)
	{
		if (IsAuthorized())
		{
			var result = await aiServicesHandler.GetBugSeverityFromAIServiceAsync(bugSeverityInput);
			if (result is not null)
			{
				return HandleSuccessRequestResponse(result);
			}

			return HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.SomethingWentWrongMessageConstant);
		}

		return HandleUnAuthorizedRequestResponse();
	}

	/// <summary>
	/// Gets the active ai features asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="AIFeaturesDTO"/></returns>
	[HttpGet(RouteConstants.AIServicesApiRoutes.GetActiveAIFeatures_ApiRoute)]
	[ProducesResponseType(typeof(IEnumerable<AIFeaturesDTO>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(Summary = GetActiveAIFeaturesAction.Summary, Description = GetActiveAIFeaturesAction.Description, OperationId = GetActiveAIFeaturesAction.OperationId)]
	public async Task<ResponseDTO> GetActiveAIFeaturesAsync()
	{
		if (IsAuthorized())
		{
			var result = await aiServicesHandler.GetActiveAIFeaturesAsync().ConfigureAwait(false);
			if (result is not null)
			{
				return HandleSuccessRequestResponse(result);
			}

			return HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.SomethingWentWrongMessageConstant);
		}

		return HandleUnAuthorizedRequestResponse();
	}

	/// <summary>
	/// Gets the sample prompts for chatbot asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="SampleChatbotPromptsDTO"/></returns>
	[HttpGet(RouteConstants.AIServicesApiRoutes.GetSamplePromptsForChatbot_ApiRoute)]
	[ProducesResponseType(typeof(IEnumerable<SampleChatbotPromptsDTO>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(Summary = GetSamplePromptsForChatbotAction.Summary, Description = GetSamplePromptsForChatbotAction.Description, OperationId = GetSamplePromptsForChatbotAction.OperationId)]
	public async Task<ResponseDTO> GetSamplePromptsForChatbotAsync()
	{
		if (IsAuthorized())
		{
			var result = await aiServicesHandler.GetSamplePromptsForChatbotAsync().ConfigureAwait(false);
			if (result is not null)
			{
				return HandleSuccessRequestResponse(result);
			}

			return HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.SomethingWentWrongMessageConstant);
		}

		return HandleUnAuthorizedRequestResponse();
	}
}
