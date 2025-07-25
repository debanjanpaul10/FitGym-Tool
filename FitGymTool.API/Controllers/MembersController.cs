// *********************************************************************************
//	<copyright file="MembersController.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Members Controller Class.</summary>
// *********************************************************************************

using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Globalization;
using static FitGymTool.API.Helpers.APIConstants;
using static FitGymTool.API.Helpers.SwaggerConstants.MembersController;

namespace FitGymTool.API.Controllers;

/// <summary>
/// The Members Controller Class.
/// </summary>
/// <param name="membersHandler">The members service.</param>
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="logger">The logger service.</param>
/// <seealso cref="BaseController"/>
[ApiController]
[Route(RouteConstants.MembersApiRoutes.BaseRoute_RoutePrefix)]
public class MembersController(IMembersHandler membersHandler, IHttpContextAccessor httpContextAccessor, ILogger<MembersController> logger) : BaseController(httpContextAccessor)
{
    /// <summary>
    /// Adds a new member to the database asynchronously.
    /// </summary>
    /// <param name="memberDetails">The member details data.</param>
    /// <param name="isFromAdmin">The boolean flag to indicate admin request.</param>
    /// <returns>The boolean result for success/failure.</returns>
    [HttpPost(RouteConstants.MembersApiRoutes.AddMember_ApiRoute)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = AddNewMemberAction.Summary, Description = AddNewMemberAction.Description, OperationId = AddNewMemberAction.OperationId)]
    public async Task<ResponseDTO> AddNewMemberAsync([FromBody] AddMemberDTO memberDetails, [FromRoute] bool isFromAdmin = false)
    {
        try
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, base.UserFullName));
            if (IsAuthorized())
            {
                base.PrepareDefaultDtoData(memberDetails);
                var result = await membersHandler.AddNewMemberAsync(memberDetails, base.UserEmail, isFromAdmin);
                if (result)
                {
                    return HandleSuccessRequestResponse(result);
                }

                return HandleBadRequestResponse(StatusCodes.Status400BadRequest, ValidationErrorMessages.MemberCouldNotBeAddedMessageConstant);
            }

            return HandleUnAuthorizedRequestResponse();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, ex.Message));
            return HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, base.UserFullName));
        }
    }

    /// <summary>
    /// Gets all members from the database asynchronously.
    /// </summary>
    /// <returns>A list of MemberDetails.</returns>
    [HttpGet(RouteConstants.MembersApiRoutes.GetAllMembers_ApiRoute)]
    [ProducesResponseType(typeof(List<MemberDetailsDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = GetAllMembersAction.Summary, Description = GetAllMembersAction.Description, OperationId = GetAllMembersAction.OperationId)]
    public async Task<ResponseDTO> GetAllMembersAsync()
    {
        try
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, base.UserFullName));
            if (IsAuthorized())
            {
                var result = await membersHandler.GetAllMembersAsync();
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
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, ex.Message));
            return HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, base.UserFullName));
        }
    }

    /// <summary>
    /// Gets a single member's details by Member's Email ID asynchronously.
    /// </summary>
    /// <param name="memberEmailId">The member's Email ID.</param>
    /// <returns>The MemberDetails object if found; otherwise, null.</returns>
    [HttpPost(RouteConstants.MembersApiRoutes.GetMemberByEmailId_ApiRoute)]
    [ProducesResponseType(typeof(MemberDetailsDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = GetMemberByEmailIdAction.Summary, Description = GetMemberByEmailIdAction.Description, OperationId = GetMemberByEmailIdAction.OperationId)]
    public async Task<ResponseDTO> GetMemberByEmailIdAsync([FromBody] string memberEmailId)
    {
        try
        {
            logger.LogInformation(string.Format(
                CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, base.UserFullName));
            if (IsAuthorized())
            {
                var result = await membersHandler.GetMemberByEmailIdAsync(memberEmailId);
                if (result is not null)
                {
                    return HandleSuccessRequestResponse(result);
                }

                return HandleBadRequestResponse(StatusCodes.Status400BadRequest, ValidationErrorMessages.MemberNotFoundMessageConstant);
            }

            return HandleUnAuthorizedRequestResponse();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, ex.Message));
            return HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, base.UserFullName));
        }
    }

    /// <summary>
    /// Updates an existing member's details asynchronously.
    /// </summary>
    /// <param name="memberDetails">The updated member details.</param>
    /// <returns>The boolean result for success/failure.</returns>
    [HttpPut(RouteConstants.MembersApiRoutes.UpdateMember_ApiRoute)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = UpdateMemberDetailsAction.Summary, Description = UpdateMemberDetailsAction.Description, OperationId = UpdateMemberDetailsAction.OperationId)]
    public async Task<ResponseDTO> UpdateMemberDetailsAsync([FromBody] UpdateMemberDTO memberDetails)
    {
        try
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(UpdateMemberDetailsAsync), DateTime.UtcNow, base.UserFullName));
            if (IsAuthorized())
            {
                base.PrepareDefaultDtoData(memberDetails);
                var result = await membersHandler.UpdateMemberDetailsAsync(memberDetails);
                if (result)
                {
                    return HandleSuccessRequestResponse(result);
                }

                return HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.SomethingWentWrongMessageConstant);
            }

            return HandleUnAuthorizedRequestResponse();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMemberDetailsAsync), DateTime.UtcNow, ex.Message));
            return HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(UpdateMemberDetailsAsync), DateTime.UtcNow, base.UserFullName));
        }
    }

    /// <summary>
    /// Updates the membership status data asynchronous.
    /// </summary>
    /// <param name="membershipStatusDto">The membership status dto.</param>
    /// <returns>The action result of the response.</returns>
    [HttpPut(RouteConstants.MembersApiRoutes.UpdateMembershipStatus_ApiRoute)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = UpdateMembershipStatusAction.Summary, Description = UpdateMembershipStatusAction.Description, OperationId = UpdateMembershipStatusAction.OperationId)]
    public async Task<ResponseDTO> UpdateMembershipStatusDataAsync([FromBody] UpdateMembershipStatusDTO membershipStatusDto)
    {
        try
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(UpdateMembershipStatusDataAsync), DateTime.UtcNow, base.UserFullName));
            if (IsAuthorized())
            {
                base.PrepareDefaultDtoData(membershipStatusDto);
                var result = await membersHandler.UpdateMembershipStatusAsync(membershipStatusDto);
                if (result)
                {
                    return HandleSuccessRequestResponse(result);
                }

                return HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.SomethingWentWrongMessageConstant);
            }

            return HandleUnAuthorizedRequestResponse();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMembershipStatusDataAsync), DateTime.UtcNow, ex.Message));
            return HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
        }
        finally
        {
            logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(UpdateMembershipStatusDataAsync), DateTime.UtcNow, base.UserFullName));
        }
    }
}
