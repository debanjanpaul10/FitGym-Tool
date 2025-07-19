// *********************************************************************************
//	<copyright file="DIContainer.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Dependency Injection Container Class.</summary>
// *********************************************************************************

using Azure.Identity;
using FitGymTool.API.Adapters.IOC;
using FitGymTool.API.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System.Security.Claims;
using FitGymTool.Domain.IOC;
using static FitGymTool.API.Helpers.APIConstants;
using FitGymTool.Persistence.Adapters.IOC;

namespace FitGymTool.API.IOC;

/// <summary>
/// The Dependency Injection Container Class.
/// </summary>
public static class DIContainer
{
	/// <summary>
	/// Configures the API services for the application, including authentication and data management dependencies.
	/// </summary>
	/// <param name="builder">The web application builder.</param>
	public static void ConfigureApiServices(this WebApplicationBuilder builder)
	{
		builder.ConfigureAuthenticationServices();
		builder.Services.AddMemoryCache();

		builder.Services.AddAPIHandlers()
			.AddDataDependencies(builder.Configuration, builder.Environment.IsDevelopment())
			.AddDomainServices();
	}

	/// <summary>
	/// Configures Azure App Configuration for the application using the provided credentials.
	/// </summary>
	/// <param name="builder">The web application builder.</param>
	/// <param name="credentials">The azure credentials.</param>
	/// <exception cref="InvalidOperationException"></exception>
	public static void ConfigureAzureAppConfiguration(this WebApplicationBuilder builder, DefaultAzureCredential credentials)
	{
		var configuration = builder.Configuration;
		var appConfigurationEndpoint = configuration[ConfigurationConstants.AppConfigurationEndpointKeyConstant];
		if (string.IsNullOrEmpty(appConfigurationEndpoint))
		{
			throw new InvalidOperationException(ExceptionConstants.ConfigurationValueIsEmptyMessageConstant);
		}

		configuration.AddAzureAppConfiguration(options =>
		{
			options.Connect(new Uri(appConfigurationEndpoint), credentials)
				.Select(KeyFilter.Any).Select(KeyFilter.Any, ConfigurationConstants.BaseConfigurationAppConfigKeyConstant)
				.Select(KeyFilter.Any, ConfigurationConstants.FitGymAPIAppConfigKeyConstant)
				.ConfigureKeyVault((options) =>
				{
					options.SetCredential(credentials);
				});
		});
	}

	

	#region PRIVATE Methods

	/// <summary>
	/// Configures the authentication services for the application.
	/// </summary>
	/// <param name="builder">The web application builder.</param>
	private static void ConfigureAuthenticationServices(this WebApplicationBuilder builder)
	{
		var configuration = builder.Configuration;
		builder.Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
			{
				ValidateLifetime = true,
				ValidateIssuer = true,
				ValidateAudience = true,
				RequireExpirationTime = true,
				RequireSignedTokens = true,
				ValidAudience = configuration[AuthenticationConstants.AzureAdApiClientIdConstant],
				ValidIssuer = configuration[AuthenticationConstants.AzureAdApiIssuerConstant],
				SignatureValidator = (token, _) => new Microsoft.IdentityModel.JsonWebTokens.JsonWebToken(token)
			};
			options.Events = new JwtBearerEvents
			{
				OnTokenValidated = HandleAuthTokenValidationSuccessAsync,
				OnAuthenticationFailed = HandleAuthTokenValidationFailedAsync
			};
		});
	}

	/// <summary>
	/// Handles auth token validation success async.
	/// </summary>
	/// <param name="context">The token validation context.</param>
	private static async Task HandleAuthTokenValidationSuccessAsync(this TokenValidatedContext context)
	{
		var claimsPrincipal = context.Principal;
		if (claimsPrincipal?.Identity is not ClaimsIdentity claimsIdentity || !claimsIdentity.IsAuthenticated)
		{
			context.Fail(ExceptionConstants.InvalidTokenExceptionConstant);
			return;
		}

		context.HttpContext.User = new ClaimsPrincipal(claimsIdentity);
		await Task.CompletedTask;
	}

	/// <summary>
	/// Handles auth token validation failed async.
	/// </summary>
	/// <param name="context">The auth failed context.</param>
	private static async Task HandleAuthTokenValidationFailedAsync(this AuthenticationFailedContext context)
	{
		var authenticationFailedException = new UnauthorizedAccessException(context.Exception.Message);
		var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<BaseController>>();
		logger.LogError(authenticationFailedException, context.Exception.Message);

		context.Fail(context.Exception.Message);
		await Task.CompletedTask;
	}


	#endregion
}
