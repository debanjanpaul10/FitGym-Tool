using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FitGymTool.API.Adapters.IOC;

public static class DependencyContainer
{
	public static IServiceCollection AddAPIHandlers(this IServiceCollection services)
	{
		services.AddScoped<IFitGymCommonHandler, FitGymCommonHandler>();

		return services;
	}
}
