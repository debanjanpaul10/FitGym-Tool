// *********************************************************************************
//	<copyright file="MemberFeesController.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Member Fees Controller Class.</summary>
// *********************************************************************************

using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Adapters.Models.Response.DerivedEntities;
using FitGymTool.API.Helpers;
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
	/// <returns>The list of <see cref="CurrentMonthFeesAndRevenueStatusDTO"/></returns>
	[HttpGet(RouteConstants.MemberFeesApiRoutes.GetCurrentMonthFeesAndRevenueStatus_ApiRoute)]
	[ProducesResponseType(typeof(IEnumerable<CurrentMonthFeesAndRevenueStatusDTO>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ResponseDTO> GetCurrentMonthFeesAndRevenueStatusAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, base.UserEmail));
			if (IsAuthorized())
			{
				var result = await memberFeesHandler.GetCurrentMonthFeesAndRevenueStatusAsync();
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
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, base.UserEmail));
		}
	}

	/// <summary>
	/// Gets the current fees structure asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="FeesStructureDTO"/></returns>
	[HttpGet(RouteConstants.MemberFeesApiRoutes.GetCurrentFeesStructure_ApiRoute)]
	[ProducesResponseType(typeof(IEnumerable<FeesStructureDTO>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ResponseDTO> GetCurrentFeesStructureAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetCurrentFeesStructureAsync), DateTime.UtcNow, base.UserEmail));
			if (IsAuthorized())
			{
				var result = await memberFeesHandler.GetCurrentFeesStructureAsync();
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
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetCurrentFeesStructureAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetCurrentFeesStructureAsync), DateTime.UtcNow, base.UserEmail));
		}
	}

	/// <summary>
	/// Gets the current members fees status asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="CurrentMembersFeesStatusDTO"/></returns>
	[HttpGet(RouteConstants.MemberFeesApiRoutes.GetCurrentMembersFeesStatus_ApiRoute)]
	[ProducesResponseType(typeof(IEnumerable<CurrentMembersFeesStatusDTO>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ResponseDTO> GetCurrentMembersFeesStatusAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetCurrentMembersFeesStatusAsync), DateTime.UtcNow, base.UserEmail));
			if (IsAuthorized())
			{
				var result = await memberFeesHandler.GetCurrentMembersFeesStatusAsync();
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
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetCurrentMembersFeesStatusAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetCurrentMembersFeesStatusAsync), DateTime.UtcNow, base.UserEmail));
		}
	}

	/// <summary>
	/// Gets the payment history data for member asynchronous.
	/// </summary>
	/// <param name="emailId">The email identifier.</param>
	/// <returns>The list of <see cref="MemberPaymentHistoryDTO"/></returns>
	[HttpGet(RouteConstants.MemberFeesApiRoutes.GetPaymentHistoryDataForMember_ApiRoute)]
	[ProducesResponseType(typeof(IEnumerable<MemberPaymentHistoryDTO>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ResponseDTO> GetPaymentHistoryDataForMemberAsync([FromQuery] string emailId)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetPaymentHistoryDataForMemberAsync), DateTime.UtcNow, base.UserEmail));
			if (IsAuthorized())
			{
				var result = await memberFeesHandler.GetPaymentHistoryDataForMemberAsync(emailId);
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
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetPaymentHistoryDataForMemberAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetPaymentHistoryDataForMemberAsync), DateTime.UtcNow, base.UserEmail));
		}
	}
}
