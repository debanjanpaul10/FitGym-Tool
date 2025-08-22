// *********************************************************************************
//	<copyright file="AIServicesController.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The AI Services Controller Class.</summary>
// *********************************************************************************

using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Globalization;
using static FitGymTool.API.Helpers.APIConstants;
using static FitGymTool.API.Helpers.SwaggerConstants.AIServicesController;

namespace FitGymTool.API.Controllers;

/// <summary>
/// The AI Services Controller Class.
/// </summary>
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="logger">The logger service.</param>
/// <param name="aiServicesHandler">The AI services handler.</param>
/// <seealso cref="FitGymTool.API.Controllers.BaseController" />
[ApiController]
[Route(RouteConstants.AIServicesApiRoutes.BaseRoute_RoutePrefix)]
public class AIServicesController(IHttpContextAccessor httpContextAccessor, ILogger<AIServicesController> logger, IAIServicesHandler aiServicesHandler) : BaseController(httpContextAccessor)
{
	/// <summary>
	/// Responds the user query asynchronous.
	/// </summary>
	/// <param name="chatMessage">The chat message.</param>
	/// <returns>The ai response.</returns>
	[HttpPost(RouteConstants.AIServicesApiRoutes.Respond_ApiRoute)]
	[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(Summary = RespondAction.Summary, Description = RespondAction.Description, OperationId = RespondAction.OperationId)]
	public async Task<ResponseDTO> RespondAsync([FromBody] ChatMessageRequestDTO chatMessage)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(RespondAsync), DateTime.UtcNow, base.UserFullName));
			if (IsAuthorized())
			{
				var result = await aiServicesHandler.GetChatbotResponseAsync(chatMessage).ConfigureAwait(false);
				if (!string.IsNullOrEmpty(result))
				{
					return HandleSuccessRequestResponse(result);
				}

				return HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.SomethingWentWrongMessageConstant);
			}

			return HandleUnAuthorizedRequestResponse();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(RespondAsync), DateTime.UtcNow, ex.Message));
			return HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(RespondAsync), DateTime.UtcNow, base.UserFullName));
		}
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
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetBugSeverityStatusAsync), DateTime.UtcNow, base.UserFullName));
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
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetBugSeverityStatusAsync), DateTime.UtcNow, ex.Message));
			return HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetBugSeverityStatusAsync), DateTime.UtcNow, base.UserFullName));
		}
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
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetActiveAIFeaturesAsync), DateTime.UtcNow, base.UserFullName));
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
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetActiveAIFeaturesAsync), DateTime.UtcNow, ex.Message));
			return HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetActiveAIFeaturesAsync), DateTime.UtcNow, base.UserFullName));
		}
	}

	/// <summary>
	/// Gets the database schema json asynchronous.
	/// </summary>
	/// <returns>The string response.</returns>
	[HttpGet(RouteConstants.AIServicesApiRoutes.GetDatabaseSchemaSql_ApiRoute)]
	[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(Summary = GetDatabaseSchemaJsonAction.Summary, Description = GetDatabaseSchemaJsonAction.Description, OperationId = GetDatabaseSchemaJsonAction.OperationId)]
	public async Task<ResponseDTO> GetDatabaseSchemaJsonAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetDatabaseSchemaJsonAsync), DateTime.UtcNow, base.UserFullName ?? string.Empty));
			var result = await aiServicesHandler.GetDatabaseSchemaJsonAsync().ConfigureAwait(false);
			if (result is not null)
			{
				return HandleSuccessRequestResponse(result);
			}

			return HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ExceptionConstants.SomethingWentWrongMessageConstant);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetDatabaseSchemaJsonAsync), DateTime.UtcNow, ex.Message));
			return HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetDatabaseSchemaJsonAsync), DateTime.UtcNow, base.UserFullName ?? string.Empty));
		}
	}

	/// <summary>
	/// Gets the database knowledge base json asynchronous.
	/// </summary>
	/// <returns>The string response.</returns>
	[HttpGet(RouteConstants.AIServicesApiRoutes.GetKnowledgeBaseSql_ApiRoute)]
	[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(Summary = GetDatabaseKnowledgeBaseJsonAction.Summary, Description = GetDatabaseKnowledgeBaseJsonAction.Description, OperationId = GetDatabaseKnowledgeBaseJsonAction.OperationId)]
	public async Task<ResponseDTO> GetDatabaseKnowledgeBaseJsonAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetDatabaseKnowledgeBaseJsonAsync), DateTime.UtcNow, base.UserFullName ?? string.Empty));
			var result = await aiServicesHandler.GetDatabaseKnowledgePiecesJsonAsync().ConfigureAwait(false);
			if (result is not null)
			{
				return HandleSuccessRequestResponse(result);
			}

			return HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ExceptionConstants.SomethingWentWrongMessageConstant);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetDatabaseKnowledgeBaseJsonAsync), DateTime.UtcNow, ex.Message));
			return HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetDatabaseKnowledgeBaseJsonAsync), DateTime.UtcNow, base.UserFullName ?? string.Empty));
		}
	}


}
