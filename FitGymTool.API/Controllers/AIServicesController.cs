// *********************************************************************************
//	<copyright file="AIServicesController.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The AI Services Controller Class.</summary>
// *********************************************************************************

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
/// <seealso cref="FitGymTool.API.Controllers.BaseController" />
[ApiController]
[Route(RouteConstants.AIServicesApiRoutes.BaseRoute_RoutePrefix)]
public class AIServicesController(IHttpContextAccessor httpContextAccessor, ILogger<AIServicesController> logger) : BaseController(httpContextAccessor)
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
                // Add 5-second delay to simulate AI processing time
                await Task.Delay(5000);
                return HandleSuccessRequestResponse("Thanks for your message! I'm still learning how to respond better.");
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
}
