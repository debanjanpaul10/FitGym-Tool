// *********************************************************************************
//	<copyright file="FitGymCommonHandler.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fit Gym Common Handler Adapter Service.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Models.Response.MappingData;
using FitGymTool.Domain.DrivingPorts;

namespace FitGymTool.API.Adapters.Handlers;

/// <summary>
/// The FitGym Common Handler Adapter Service.
/// </summary>
/// <param name="fitGymCommonService">The FitGym Common Service.</param>
/// <param name="mapper">The Automapper.</param>
/// <seealso cref="FitGymTool.API.Adapters.Contracts.ICommonHandler" />
public class CommonHandler(ICommonService fitGymCommonService, IMapper mapper) : ICommonHandler
{
	/// <summary>
	/// The common service
	/// </summary>
	private readonly ICommonService _commonService = fitGymCommonService;

	/// <summary>
	/// The mapper
	/// </summary>
	private readonly IMapper _mapper = mapper;

	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>
	/// The mapping master data dto.
	/// </returns>
	public async Task<MappingMasterDataDto> GetMappingsMasterDataAsync()
	{
		var mappingsMasterData = await this._commonService.GetMappingsMasterDataAsync();
		return this._mapper.Map<MappingMasterDataDto>(mappingsMasterData);
	}
}
