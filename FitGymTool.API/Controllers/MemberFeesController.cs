// *********************************************************************************
//	<copyright file="MemberFeesController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Member Fees Controller Class.</summary>
// *********************************************************************************


using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Helpers;
using FitGymTool.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static FitGymTool.API.Helpers.APIConstants;

namespace FitGymTool.API.Controllers;

/// <summary>
/// The Member Fees Controller Class.
/// </summary>
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="logger">The logger.</param>
/// <param name="memberFeesHandler">The member fees service.</param>
/// <seealso cref="FitGymTool.API.Controllers.BaseController" />
[ApiController]
[Route(RouteConstants.MemberFeesApiRoutes.BaseRoute_RoutePrefix)]
public class MemberFeesController(IHttpContextAccessor httpContextAccessor, IMemberFeesHandler memberFeesHandler, ILogger<MemberFeesController> logger) : BaseController(httpContextAccessor)
{
	/// <summary>
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>The response data dto.</returns>
	[HttpGet(RouteConstants.MemberFeesApiRoutes.GetCurrentMonthFeesAndRevenueStatus_ApiRoute)]
	[ProducesResponseType(typeof(CurrentMonthFeesAndRevenueStatusDomain), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ResponseDTO> GetCurrentMonthFeesAndRevenueStatusAsync()
	{
		try
		{
			logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
			if (this.IsAuthorized())
			{
				var result = await memberFeesHandler.GetCurrentMonthFeesAndRevenueStatusAsync();
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
			logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
		}
	}
}
