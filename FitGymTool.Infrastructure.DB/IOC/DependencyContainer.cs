using FitGymTool.Infrastructure.DB.Contracts;
using FitGymTool.Infrastructure.DB.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static FitGymTool.Infrastructure.DB.Helpers.Constants.DatabaseConstants;

namespace FitGymTool.Infrastructure.DB.IOC;

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

	private static IServiceCollection AddDataManagers(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>()
			.AddScoped<IMembersDataService, MembersDataService>()
			.AddScoped<IFitGymCommonDataService, FitGymCommonDataService>()
			.AddScoped<IMemberFeesDataService, MemberFeesDataService>();

		return services;
	}
}
