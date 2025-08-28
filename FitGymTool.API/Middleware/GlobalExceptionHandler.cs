// *********************************************************************************
//	<copyright file="GlobalExceptionHandler.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Global Exception Handler.</summary>
// *********************************************************************************

using FitGymTool.Domain.Helpers;
using Microsoft.AspNetCore.Diagnostics;
using static FitGymTool.API.Helpers.APIConstants;

namespace FitGymTool.API.Middleware;

/// <summary>
/// The Global Exception Handler class implements the IExceptionHandler interface to handle exceptions globally in the ASP.NET Core pipeline.
/// </summary>
/// <param name="logger">The logger.</param>
/// <seealso cref="IExceptionHandler"/>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, RequestDelegate next)
{
	/// <summary>
	/// Invokes the specified HTTP context.
	/// </summary>
	/// <param name="httpContext">The HTTP context.</param>
	public async Task Invoke(HttpContext httpContext)
	{
		try
		{
			await next(httpContext);
		}
		catch (UnauthorizedAccessException ex)
		{
			await HandleExceptionAsync(httpContext, ex, StatusCodes.Status401Unauthorized, ex.ToString(), ex.Message);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(httpContext, ex, StatusCodes.Status500InternalServerError, ex.ToString(), ex.Message);
		}
	}

	/// <summary>
	/// Handles the exception asynchronous.
	/// </summary>
	/// <param name="httpContext">The HTTP context.</param>
	/// <param name="ex">The ex.</param>
	/// <param name="statusCode">The status code.</param>
	/// <param name="error">The error.</param>
	/// <param name="message">The message.</param>
	public async Task HandleExceptionAsync(HttpContext httpContext, Exception ex, int statusCode, string error, string message)
	{
		logger.LogError(ex, string.Format(LoggingConstants.MethodFailedWithMessageConstant, httpContext.Request.Method, DateTime.UtcNow, ex.Message));

		httpContext.Response.ContentType = ConfigurationConstants.ApplicationJsonConstant;
		httpContext.Response.StatusCode = statusCode;

		var errorResponse = new FitGymToolExceptions(message, statusCode, error);
		await httpContext.Response.WriteAsJsonAsync(errorResponse);
	}
}
