// *********************************************************************************
//	<copyright file="Program.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Program Class.</summary>
// *********************************************************************************

using Azure.Identity;
using FitGymTool.API.IOC;
using FitGymTool.API.Middleware;
using Microsoft.OpenApi.Models;
using static FitGymTool.API.Helpers.APIConstants;

namespace FitGymTool.API;

/// <summary>
/// The Program Class.
/// </summary>
public static class Program
{
	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	/// <param name="args">The command line arguments.</param>
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.ConfigureServices();

		var app = builder.Build();
		app.ConfigureApplication();
	}

	/// <summary>
	/// Configures the services for the application.
	/// </summary>
	/// <param name="builder">The web application builder.</param>
	internal static void ConfigureServices(this WebApplicationBuilder builder)
	{
		builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(path: ConfigurationConstants.DevelopmentAppSettingsFile, optional: true).AddEnvironmentVariables();
		var credentials = builder.Environment.IsDevelopment()
			? new DefaultAzureCredential()
			: new DefaultAzureCredential(new DefaultAzureCredentialOptions
			{
				ManagedIdentityClientId = builder.Configuration[ConfigurationConstants.ManagedIdentityClientIdConstant]
			});

		builder.Services.AddControllers();
		builder.Services.AddOpenApi();
		builder.Services.AddCors(options =>
		{
			options.AddDefaultPolicy(policy =>
			{
				policy.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
			});
		});

		builder.Services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc(SwaggerConstants.ApiVersion, new OpenApiInfo
			{
				Title = SwaggerConstants.ApplicationAPIName,
				Version = SwaggerConstants.ApiVersion,
				Description = SwaggerConstants.SwaggerDescription,
				Contact = new OpenApiContact
				{
					Name = SwaggerConstants.AuthorDetails.Name,
					Email = SwaggerConstants.AuthorDetails.Email
				}

			});
		});


		builder.ConfigureAzureAppConfiguration(credentials);
		builder.ConfigureApiServices();

		builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
		builder.Services.AddProblemDetails();
		builder.Services.AddHttpContextAccessor();

	}

	/// <summary>
	/// Configures the application middleware and endpoints.
	/// </summary>
	/// <param name="app">The web application.</param>
	internal static void ConfigureApplication(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.MapOpenApi();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint(SwaggerConstants.SwaggerEndpointUrl, $"{SwaggerConstants.ApplicationAPIName}.{SwaggerConstants.ApiVersion}");
				c.RoutePrefix = SwaggerConstants.SwaggerUiPrefix;
			});
		}

		app.UseExceptionHandler();
		app.UseHttpsRedirection();
		app.UseCors();

		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllers();

		app.Run();
	}
}
