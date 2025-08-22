// *********************************************************************************
//	<copyright file="DependencyContainer.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Dependency Injection Container Class.</summary>
// *********************************************************************************

using FitGymTool.AIAgents.Adapters.ServiceManager;
using FitGymTool.AIAgents.Adapters.Utilities;
using FitGymTool.Domain.DrivenPorts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static FitGymTool.AIAgents.Adapters.Helpers.Constants;

namespace FitGymTool.AIAgents.Adapters.IOC;

/// <summary>
/// The DI Container class.
/// </summary>
public static class DependencyContainer
{
	/// <summary>
	/// Adds the ai agents services.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <param name="configuration">The configuration.</param>
	/// <returns>The service collection.</returns>
	public static IServiceCollection AddAiAgentsServices(this IServiceCollection services, IConfiguration configuration)
	{
		return services.AddScoped<IHttpClientHelper, HttpClientHelper>()
			.AddScoped<IAIServicesManager, AIServicesManager>()
			.ConfigureHttpClientFactory(configuration);
	}

	/// <summary>
	/// Configures the HTTP client factory.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <param name="configuration">The configuration.</param>
	private static IServiceCollection ConfigureHttpClientFactory(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient(ConfigurationConstants.AiAgentsHttpClient, client =>
		{
			var apiBaseAddress = configuration[ConfigurationConstants.AiAgentsApiBaseUrl];
			if (string.IsNullOrEmpty(apiBaseAddress))
			{
				throw new ArgumentNullException(apiBaseAddress);
			}

			apiBaseAddress = "https://localhost:8190/aiagentsapi/";
			client.BaseAddress = new Uri(apiBaseAddress);
			client.Timeout = TimeSpan.FromMinutes(3);
		});

		return services;
	}
}
