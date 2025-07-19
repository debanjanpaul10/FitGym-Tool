// *********************************************************************************
//	<copyright file="BaseController.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Base Controller Class.</summary>
// *********************************************************************************

using FitGymTool.API.Adapters.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FitGymTool.API.Helpers;
using FitGymTool.API.Adapters.Models.Request;

namespace FitGymTool.API.Controllers;

/// <summary>
/// The Base Controller Class.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
/// <param name="configuration">The Configuration.</param>
[Authorize]
public abstract class BaseController : ControllerBase
{
	/// <summary>
	/// The Current User Name.
	/// </summary>
	protected string UserFullName = string.Empty;

	/// <summary>
	/// The Current User Email.
	/// </summary>
	protected string UserEmail = string.Empty;

	/// <summary>
	/// Initializes a new instance of the <see cref="BaseController"/> class.
	/// </summary>
	protected BaseController(IHttpContextAccessor httpContextAccessor)
	{
		if (httpContextAccessor.HttpContext is not null && httpContextAccessor.HttpContext?.User is not null)
		{
			var userFullName = httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type.Equals(APIConstants.AuthenticationConstants.UserFullNameClaimConstant))?.Value;
			if (!string.IsNullOrEmpty(userFullName))
			{
				UserFullName = userFullName;
			}
			else
			{
				UserFullName = APIConstants.HeaderConstants.NotApplicableStringConstant;
			}

			var userEmail = httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type.Equals(APIConstants.AuthenticationConstants.UserEmailClaimConstant))?.Value;
			if (!string.IsNullOrEmpty(userEmail))
			{
				UserEmail = userEmail;
			}
			else
			{
				UserEmail = APIConstants.HeaderConstants.NotApplicableStringConstant;
			}
		}
	}

	/// <summary>
	/// Prepares the success response.
	/// </summary>
	/// <param name="responseData">The response data.</param>
	/// <returns>The response DTO.</returns>
	protected ResponseDTO HandleSuccessRequestResponse(object responseData)
	{
		return new ResponseDTO()
		{
			IsSuccess = true,
			ResponseData = responseData,
			StatusCode = StatusCodes.Status200OK,
		};
	}

	/// <summary>
	/// Handles the bad request response.
	/// </summary>
	/// <param name="statusCode">The status code.</param>
	/// <param name="message">The message.</param>
	/// <returns>The response DTO.</returns>
	protected ResponseDTO HandleBadRequestResponse(int statusCode, string message)
	{
		return new ResponseDTO()
		{
			IsSuccess = false,
			ResponseData = message,
			StatusCode = statusCode,
		};
	}

	/// <summary>
	/// Handles the unauthorized request response.
	/// </summary>
	/// <returns>The response DTO.</returns>
	protected ResponseDTO HandleUnAuthorizedRequestResponse()
	{
		return new ResponseDTO()
		{
			IsSuccess = false,
			ResponseData = APIConstants.ExceptionConstants.UnauthorizedAccessMessageConstant,
			StatusCode = StatusCodes.Status401Unauthorized,
		};
	}

	/// <summary>
	/// Handles the user authentication response.
	/// </summary>
	/// <returns>The boolean for authentication.</returns>
	protected bool IsAuthorized()
	{
		if (!string.IsNullOrEmpty(UserFullName) && !string.IsNullOrEmpty(UserEmail))
		{
			return true;
		}

		return false;
	}

	/// <summary>
	/// Prepares the default dto data.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="inputDto">The input dto.</param>
	protected void PrepareDefaultDtoData<T>(T inputDto) where T: BaseDTO
	{
		inputDto.DateModified = DateTime.Now;
		inputDto.ModifiedBy = UserEmail;
	}
}
