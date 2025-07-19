// *********************************************************************************
//	<copyright file="GlobalExceptionHandler.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Global Exception Handler.</summary>
// *********************************************************************************

using FitGymTool.Domain.Helpers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FitGymTool.API.Middleware;

/// <summary>
/// The Global Exception Handler class implements the IExceptionHandler interface to handle exceptions globally in the ASP.NET Core pipeline.
/// </summary>
/// <param name="logger">The logger.</param>
/// <seealso cref="IExceptionHandler"/>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
	// <summary>
	/// The logger
	/// </summary>
	private readonly ILogger<GlobalExceptionHandler> _logger = logger;

	/// <summary>
	/// Tries to handle the specified exception asynchronously within the ASP.NET Core pipeline.
	/// Implementations of this method can provide custom exception-handling logic for different scenarios.
	/// </summary>
	/// <param name="httpContext">The <see cref="T:Microsoft.AspNetCore.Http.HttpContext" /> for the request.</param>
	/// <param name="exception">The unhandled exception.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>
	/// A task that represents the asynchronous read operation. The value of its <see cref="P:System.Threading.Tasks.ValueTask`1.Result" />
	/// property contains the result of the handling operation.
	/// <see langword="true" /> if the exception was handled successfully; otherwise <see langword="false" />.
	/// </returns>
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		var problemDetails = new ProblemDetails
		{
			Instance = httpContext.Request.Path
		};
		if (exception is FitGymToolExceptions ex)
		{
			httpContext.Response.StatusCode = ex.StatusCode;
			problemDetails.Title = ex.Message;
		}
		else
		{
			problemDetails.Title = exception.Message;
		}

		_logger.LogError(problemDetails.Title);
		problemDetails.Status = httpContext.Response.StatusCode;
		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);

		return true;
	}
}
