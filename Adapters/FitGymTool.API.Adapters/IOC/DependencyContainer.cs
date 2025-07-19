// *********************************************************************************
//	<copyright file="DependencyContainer.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The DI Container Class.</summary>
// *********************************************************************************

using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Handlers;
using FitGymTool.API.Adapters.Mapper;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FitGymTool.API.Adapters.IOC;

/// <summary>
/// The DI Container Class.
/// </summary>
[ExcludeFromCodeCoverage]
public static class DependencyContainer
{
	/// <summary>
	/// Adds the API handlers.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <returns>The service collection.</returns>
	public static IServiceCollection AddAPIHandlers(this IServiceCollection services)
	{
		services.AddScoped<ICommonHandler, CommonHandler>()
			.AddScoped<IMemberFeesHandler, MemberFeesHandler>()
			.AddScoped<IMembersHandler, MembersHandler>()
			.AddAutoMapper(mapperConfig =>
			{
				mapperConfig.AddProfile<DomainMapperProfile>();
			});

		return services;
	}
}
