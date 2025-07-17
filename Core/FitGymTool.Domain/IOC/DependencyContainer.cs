// *********************************************************************************
//	<copyright file="DependencyContainer.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Dependency Injection Container Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.Ports.In;
using FitGymTool.Domain.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace FitGymTool.Domain.IOC;

/// <summary>
/// The Dependency Injection Container Class.
/// </summary>
public static class DependencyContainer
{
	/// <summary>
	/// Adds the domain services.
	/// </summary>
	/// <param name="services">The services.</param>
	/// <returns>The service collection data.</returns>
	public static IServiceCollection AddDomainServices(this IServiceCollection services)
	{
		services.AddScoped<ICommonService, CommonService>()
			.AddScoped<IMemberFeesService, MemberFeesService>()
			.AddScoped<IMembersService, MembersService>();

		return services;
	}
}
