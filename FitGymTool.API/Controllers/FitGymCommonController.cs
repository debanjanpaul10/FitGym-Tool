// *********************************************************************************
//	<copyright file="FitGymCommonController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fit Gym Common Controller Class.</summary>
// *********************************************************************************

using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Adapters.Models.Response.MappingData;
using FitGymTool.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static FitGymTool.API.Helpers.APIConstants;

namespace FitGymTool.API.Controllers;

/// <summary>
/// The Fit Gym Common Controller Class.
/// </summary>
/// <param name="fitGymCommonHandler">The fit gym common service.</param>
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="logger">The logger.</param>
/// <seealso cref="FitGymTool.API.Controllers.BaseController" />
[ApiController]
[Route(RouteConstants.FitGymCommonApiRoutes.BaseRoute_RoutePrefix)]
public class FitGymCommonController(IFitGymCommonHandler fitGymCommonHandler, ILogger<FitGymCommonController>  logger, IHttpContextAccessor httpContextAccessor): BaseController(httpContextAccessor)
{
	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>The response data dto.</returns>
	[HttpGet(RouteConstants.FitGymCommonApiRoutes.GetMappingsMasterData_ApiRoute)]
	[ProducesResponseType(typeof(MappingMasterDataDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ResponseDTO> GetMappingsMasterDataAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, base.UserFullName));
			if (this.IsAuthorized())
			{
				var result = await fitGymCommonHandler.GetMappingsMasterDataAsync();
				if (result is not null)
				{
					return this.HandleSuccessRequestResponse(result);
				}

				return this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.SomethingWentWrongMessageConstant);
			}

			return this.HandleUnAuthorizedRequestResponse();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, ex.Message));
			return this.HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, base.UserFullName));
		}
	}
}
