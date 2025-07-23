// *********************************************************************************
//	<copyright file="MemberFeesHandler.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Member Fees Handler Adapter Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Adapters.Models.Response.DerivedEntities;
using FitGymTool.Domain.Ports.In;

namespace FitGymTool.API.Adapters.Handlers;

/// <summary>
/// The Member Fees Handler Adapter Class.
/// </summary>
/// <param name="mapper">The auto mapper.</param>
/// <param name="memberFeesService">The member fees service.</param>
/// <seealso cref="FitGymTool.API.Adapters.Contracts.IMemberFeesHandler" />
public class MemberFeesHandler(IMemberFeesService memberFeesService, IMapper mapper) : IMemberFeesHandler
{
	/// <summary>
	/// The member fees service
	/// </summary>
	private readonly IMemberFeesService _memberFeesService = memberFeesService;

	/// <summary>
	/// The mapper
	/// </summary>
	private readonly IMapper _mapper = mapper;

	/// <summary>
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>
	/// The list of current month fees and revenue status.
	/// </returns>
	public async Task<IEnumerable<CurrentMonthFeesAndRevenueStatusDTO>> GetCurrentMonthFeesAndRevenueStatusAsync()
	{
		var feesAndRevenueStatusData = await _memberFeesService.GetCurrentMonthFeesAndRevenueStatusAsync();
		return _mapper.Map<IEnumerable<CurrentMonthFeesAndRevenueStatusDTO>>(feesAndRevenueStatusData);
	}

	/// <summary>
	/// Gets the current fees structure asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="FeesStructureDomain"/></returns>
	public async Task<IEnumerable<FeesStructureDTO>> GetCurrentFeesStructureAsync()
	{
		var feesStructureDomainData = await _memberFeesService.GetCurrentFeesStructureAsync();
		return _mapper.Map<IEnumerable<FeesStructureDTO>>(feesStructureDomainData);
	}

	/// <summary>
	/// Gets the current members fees status asynchronous.
	/// </summary>
	/// <returns>
	/// The list of <see cref="CurrentMembersFeesStatusDTO" />
	/// </returns>
	public async Task<IEnumerable<CurrentMembersFeesStatusDTO>> GetCurrentMembersFeesStatusAsync()
	{
		var feesStatusDomainData = await _memberFeesService.GetCurrentMembersFeesStatusAsync();
		return _mapper.Map<IEnumerable<CurrentMembersFeesStatusDTO>>(feesStatusDomainData);
	}
}
