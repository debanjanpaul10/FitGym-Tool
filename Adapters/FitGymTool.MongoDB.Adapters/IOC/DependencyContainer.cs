// *********************************************************************************
//	<copyright file="DependencyContainer.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Dependency Container Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DrivenPorts;
using FitGymTool.MongoDB.Adapters.MongoDbManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using static FitGymTool.MongoDB.Adapters.Helpers.Constants;

namespace FitGymTool.MongoDB.Adapters.IOC;

/// <summary>
/// The Dependency Container Class.
/// </summary>
public static class DependencyContainer
{
    /// <summary>
    /// Adds the mongo database adapter dependencies.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddMongoDbAdapterDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        return services.ConfigureMongoDbServer(configuration).AddScoped<IMongoDbDatabaseManager, MongoDbDatabaseManager>();
    }

    /// <summary>
    /// Configures the mongo database server.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The service collection.</returns>
    private static IServiceCollection ConfigureMongoDbServer(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoConnectionString = configuration[ConfigurationConstants.MongoDbConnectionStringConstant];
        if (!string.IsNullOrEmpty(mongoConnectionString))
        {
            try
            {
                var settings = MongoClientSettings.FromConnectionString(mongoConnectionString);
                settings.UseTls = true;
                settings.SslSettings = new SslSettings()
                {
                    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13,
                    CheckCertificateRevocation = false
                };

                services.AddSingleton<IMongoClient>(new MongoClient(settings));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to configure MongoDB client: {ex.Message}", ex);
            }
        }

        return services;
    }
}
