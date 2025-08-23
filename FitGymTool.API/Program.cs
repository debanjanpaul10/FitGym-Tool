// *********************************************************************************
//	<copyright file="Program.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Program Class.</summary>
// *********************************************************************************

using Azure.Identity;
using FitGymTool.API.IOC;
using FitGymTool.API.Middleware;
using Microsoft.OpenApi.Models;
using static FitGymTool.API.Helpers.APIConstants;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(path: ConfigurationConstants.DevelopmentAppSettingsFile, optional: true).AddEnvironmentVariables();

var credentials = builder.Environment.IsDevelopment()
	? new DefaultAzureCredential()
	: new DefaultAzureCredential(new DefaultAzureCredentialOptions
	{
		ManagedIdentityClientId = builder.Configuration[ConfigurationConstants.ManagedIdentityClientIdConstant]
	});

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

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
	options.EnableAnnotations();
});


builder.ConfigureAzureAppConfiguration(credentials);
builder.ConfigureApiServices();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
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

