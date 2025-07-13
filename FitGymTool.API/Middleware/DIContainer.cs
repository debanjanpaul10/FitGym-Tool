// *********************************************************************************
//	<copyright file="DIContainer.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Dependency Injection Container Class.</summary>
// *********************************************************************************

using FitGymTool.Business.Contracts;
using FitGymTool.Business.Services;
using FitGymTool.DataAccess;
using FitGymTool.DataAccess.Contracts;
using FitGymTool.DataAccess.Services;
using Microsoft.EntityFrameworkCore;
using static FitGymTool.Shared.Constants.ConfigurationConstants;

namespace FitGymTool.API.Middleware;

/// <summary>
/// The Dependency Injection Container Class.
/// </summary>
public static class DIContainer
{
	/// <summary>
	/// Configures the SQL database services for the application.
	/// </summary>
	/// <param name="builder">The web application builder.</param>
	public static void ConfigureSqlDatabase(this WebApplicationBuilder builder)
	{
		var sqlConnectionString = builder.Environment.IsDevelopment()
			? builder.Configuration[LocalSqlConnectionStringConstant]
			: builder.Configuration[AzureSqlConnectionStringConstant];
		if (!string.IsNullOrEmpty(sqlConnectionString))
		{
			builder.Services.AddDbContext<SqlDbContext>(options =>
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
		}
	}

	/// <summary>
	/// Adds the business manager dependencies to the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	public static void AddBusinessManagerDependencies(this IServiceCollection services)
	{
		services.AddScoped<IMembersService, MembersService>();
		services.AddScoped<IFitGymCommonService, FitGymCommonService>();
	}

	/// <summary>
	/// Adds the data manager dependencies to the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	public static void AddDataManagerDependencies(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IMembersDataService, MembersDataService>();
		services.AddScoped<IMemberFeesDataService, MemberFeesDataService>();
		services.AddScoped<IFitGymCommonDataService, FitGymCommonDataService>();
	}
}
