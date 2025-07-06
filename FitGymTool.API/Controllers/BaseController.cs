// *********************************************************************************
//	<copyright file="BaseController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Base Controller Class.</summary>
// *********************************************************************************

using FitGymTool.Shared.Constants;
using FitGymTool.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitGymTool.API.Controllers;

/// <summary>
/// The Base Controller Class.
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
/// <param name="configuration">The Configuration.</param>
[Authorize]
public abstract class BaseController : ControllerBase
{
	protected string UserName = string.Empty;

	/// <summary>
	/// Initializes a new instance of the <see cref="BaseController"/> class.
	/// </summary>
	protected BaseController(IHttpContextAccessor httpContextAccessor)
	{
		if (httpContextAccessor.HttpContext is not null && httpContextAccessor.HttpContext?.User is not null)
		{
			var userName = httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type.Equals(ConfigurationConstants.UserNameClaimConstant))?.Value;
			if (!string.IsNullOrEmpty(userName))
			{
				this.UserName = userName;
			}
			else
			{
				this.UserName = "NA";
			}
		}
	}

	/// <summary>
	/// Prepares the success response.
	/// </summary>
	/// <param name="responseData">The response data.</param>
	/// <returns>The response DTO.</returns>
	protected ResponseDTO PrepareSuccessResponse(object responseData)
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
	/// Handles the user authentication response.
	/// </summary>
	/// <returns>The boolean for authentication.</returns>
	protected bool IsAuthorized()
	{
		if (!string.IsNullOrEmpty(this.UserName))
		{
			return true;
		}

		return false;
	}
}
