// *********************************************************************************
//	<copyright file="DependencyContainer.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The DI Container Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.Ports.Out;
using FitGymTool.Persistence.Adapters.Contracts;
using FitGymTool.Persistence.Adapters.DataManager;
using FitGymTool.Persistence.Adapters.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using static FitGymTool.Persistence.Adapters.Helpers.Constants.DatabaseConstants;

namespace FitGymTool.Persistence.Adapters.IOC;

/// <summary>
/// The Dependency Container Class.
/// </summary>
[ExcludeFromCodeCoverage]
public static class DependencyContainer
{
	/// <summary>
	/// Adds the data dependencies to the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration configuration, bool isDevelopmentMode)
	{
		services.ConfigureSqlDatabase(configuration, isDevelopmentMode).AddDataManagers();
		return services;
	}

	/// <summary>
	/// Configures the SQL database.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <param name="configuration">The configuration.</param>
	/// <param name="isDevelopmentMode">if set to <c>true</c> [is development mode].</param>
	/// <returns>The service collection.</returns>
	/// <exception cref="ArgumentNullException">sqlConnectionString</exception>
	private static IServiceCollection ConfigureSqlDatabase(this IServiceCollection services, IConfiguration configuration, bool isDevelopmentMode)
	{
		var sqlConnectionString = isDevelopmentMode
			? configuration[ConfigurationConstants.LocalSqlConnectionStringConstant]
			: configuration[ConfigurationConstants.AzureSqlConnectionStringConstant];
		if (string.IsNullOrEmpty(sqlConnectionString))
		{
			throw new ArgumentNullException(nameof(sqlConnectionString), ErrorMessages.DatabaseConnectionNotFound);
		}
		services.AddDbContext<SqlDbContext>(options =>
		{
			options.UseSqlServer(
				connectionString: sqlConnectionString,
				options => options.EnableRetryOnFailure(
					maxRetryCount: 3,
					maxRetryDelay: TimeSpan.FromSeconds(30),
					errorNumbersToAdd: null
				)
			);
		});

		return services;
	}

	/// <summary>
	/// Adds the data managers.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <returns></returns>
	private static IServiceCollection AddDataManagers(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>()
			.AddScoped<IMembersDataManager, MembersDataManager>()
			.AddScoped<ICommonDataManager, CommonDataManager>()
			.AddScoped<IMemberFeesDataManager, MemberFeesDataManager>()
			.AddAutoMapper(mapperConfig =>
			{
				mapperConfig.AddProfile<DataMapperProfile>();
			});

		return services;
	}
}
