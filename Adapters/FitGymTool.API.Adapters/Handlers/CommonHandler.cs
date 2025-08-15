// *********************************************************************************
//	<copyright file="FitGymCommonHandler.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Fit Gym Common Handler Adapter Service.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Adapters.Models.Response.MappingData;
using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.AIEntities;
using FitGymTool.Domain.DrivingPorts;

namespace FitGymTool.API.Adapters.Handlers;

/// <summary>
/// The FitGym Common Handler Adapter Service.
/// </summary>
/// <param name="commonService">The Common Service.</param>
/// <param name="mapper">The Automapper.</param>
/// <seealso cref="FitGymTool.API.Adapters.Contracts.ICommonHandler" />
public class CommonHandler(ICommonService commonService, IMapper mapper) : ICommonHandler
{
	/// <summary>
	/// Adds the new bug report data asynchronous.
	/// </summary>
	/// <param name="bugReportDataDTO">The bug report data domain.</param>
	/// <returns>
	/// The boolean for success/failure
	/// </returns>
	public async Task<bool> AddNewBugReportDataAsync(AddBugReportDTO bugReportDataDTO)
	{
		var bugReportData = mapper.Map<BugReportData>(bugReportDataDTO);
		return await commonService.AddNewBugReportDataAsync(bugReportData);
	}

	/// <summary>
	/// Gets the bug severity from ai service asynchronous.
	/// </summary>
	/// <param name="bugSeverityInput">The bug severity input.</param>
	/// <returns>
	/// The bug severity response.
	/// </returns>
	public async Task<BugSeverityResponseDTO> GetBugSeverityFromAIServiceAsync(BugSeverityInputDTO bugSeverityInput)
	{
		var domainInputData = mapper.Map<BugSeverityInput>(bugSeverityInput);
		var domainResponse = await commonService.GetBugSeverityFromAIServiceAsync(domainInputData).ConfigureAwait(false);
		return mapper.Map<BugSeverityResponseDTO>(domainResponse);
	}

	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>
	/// The mapping master data dto.
	/// </returns>
	public async Task<MappingMasterDataDto> GetMappingsMasterDataAsync()
	{
		var mappingsMasterData = await commonService.GetMappingsMasterDataAsync();
		return mapper.Map<MappingMasterDataDto>(mappingsMasterData);
	}
}
