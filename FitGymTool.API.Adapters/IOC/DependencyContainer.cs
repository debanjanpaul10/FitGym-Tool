// *********************************************************************************
//	<copyright file="DependencyContainer.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The DI Container Class.</summary>
// *********************************************************************************

using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Handlers;
using FitGymTool.API.Adapters.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace FitGymTool.API.Adapters.IOC;

/// <summary>
/// The DI Container Class.
/// </summary>
public static class DependencyContainer
{
	/// <summary>
	/// Adds the API handlers.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <returns>The service collection.</returns>
	public static IServiceCollection AddAPIHandlers(this IServiceCollection services)
	{
		services.AddScoped<IFitGymCommonHandler, FitGymCommonHandler>()
			.AddScoped<IMemberFeesHandler, MemberFeesHandler>()
			.AddScoped<IMembersHandler, MembersHandler>()
			.AddAutoMapper(mapperConfig =>
			{
				mapperConfig.AddProfile<DomainMapperProfile>();
			});

		return services;
	}
}
