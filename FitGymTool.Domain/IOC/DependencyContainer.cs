using FitGymTool.Domain.DrivingPorts;
using FitGymTool.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FitGymTool.Domain.IOC;

public static class DependencyContainer
{
	public static IServiceCollection AddDomainServices(IServiceCollection services)
	{
		services.AddScoped<IFitGymCommonService, FitGymCommonService>()
			.AddScoped<IMemberFeesService, MemberFeesService>()
			.AddScoped<IMembersService, MembersService>();

		return services;
	}
}
