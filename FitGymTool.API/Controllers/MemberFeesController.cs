// *********************************************************************************
//	<copyright file="MemberFeesController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Member Fees Controller Class.</summary>
// *********************************************************************************


using FitGymTool.Business.Contracts;
using FitGymTool.Shared.Constants;
using FitGymTool.Shared.DTOs;
using FitGymTool.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FitGymTool.API.Controllers;

/// <summary>
/// The Member Fees Controller Class.
/// </summary>
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="logger">The logger.</param>
/// <param name="memberFeesService">The member fees service.</param>
/// <seealso cref="FitGymTool.API.Controllers.BaseController" />
[ApiController]
[Route(RouteConstants.MemberFeesApiRoutes.BaseRoute_RoutePrefix)]
public class MemberFeesController(IHttpContextAccessor httpContextAccessor, IMemberFeesService memberFeesService, ILogger<MemberFeesController> logger) : BaseController(httpContextAccessor)
{
	/// <summary>
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>The response data dto.</returns>
	[HttpGet(RouteConstants.MemberFeesApiRoutes.GetCurrentMonthFeesAndRevenueStatus_ApiRoute)]
	[ProducesResponseType(typeof(CurrentMonthFeesAndRevenueStatus), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ResponseDTO> GetCurrentMonthFeesAndRevenueStatusAsync()
	{
		try
		{
			logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, FitGymToolConstants.NotApplicableStringConstant));
			if (this.IsAuthorized())
			{
				var result = await memberFeesService.GetCurrentMonthFeesAndRevenueStatusAsync();
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
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodEndedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, FitGymToolConstants.NotApplicableStringConstant));
		}
	}
}
