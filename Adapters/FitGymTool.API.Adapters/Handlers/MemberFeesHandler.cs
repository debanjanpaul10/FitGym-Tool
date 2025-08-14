// *********************************************************************************
//	<copyright file="MemberFeesHandler.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Member Fees Handler Adapter Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.API.Adapters.Contracts;
using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Adapters.Models.Response.DerivedEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;
using FitGymTool.Domain.DrivingPorts;

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
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>
	/// The list of current month fees and revenue status.
	/// </returns>
	public async Task<IEnumerable<CurrentMonthFeesAndRevenueStatusDTO>> GetCurrentMonthFeesAndRevenueStatusAsync()
	{
		var feesAndRevenueStatusData = await memberFeesService.GetCurrentMonthFeesAndRevenueStatusAsync();
		return mapper.Map<IEnumerable<CurrentMonthFeesAndRevenueStatusDTO>>(feesAndRevenueStatusData);
	}

	/// <summary>
	/// Gets the current fees structure asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="FeesStructureDomain"/></returns>
	public async Task<IEnumerable<FeesStructureDTO>> GetCurrentFeesStructureAsync()
	{
		var feesStructureDomainData = await memberFeesService.GetCurrentFeesStructureAsync();
		return mapper.Map<IEnumerable<FeesStructureDTO>>(feesStructureDomainData);
	}

	/// <summary>
	/// Gets the current members fees status asynchronous.
	/// </summary>
	/// <returns>
	/// The list of <see cref="CurrentMembersFeesStatusDTO" />
	/// </returns>
	public async Task<IEnumerable<CurrentMembersFeesStatusDTO>> GetCurrentMembersFeesStatusAsync()
	{
		var feesStatusDomainData = await memberFeesService.GetCurrentMembersFeesStatusAsync();
		return mapper.Map<IEnumerable<CurrentMembersFeesStatusDTO>>(feesStatusDomainData);
	}

	/// <summary>
	/// Gets the payment history data for member asynchronous.
	/// </summary>
	/// <param name="userEmailId">The user email address.</param>
	/// <returns>
	/// The list of <see cref="MemberPaymentHistoryData" />
	/// </returns>
	public async Task<IEnumerable<MemberPaymentHistoryDTO>> GetPaymentHistoryDataForMemberAsync(string userEmailId)
	{
		var memberPaymentHistoryData = await memberFeesService.GetPaymentHistoryDataForMemberAsync(userEmailId);
		return mapper.Map<IEnumerable<MemberPaymentHistoryDTO>>(memberPaymentHistoryData);
	}

	/// <summary>
	/// Updates the member fees data asynchronous.
	/// </summary>
	/// <param name="memberFeesData">The member fees data.</param>
	/// <param name="currentUserAlias">The current user alias.</param>
	/// <returns>
	/// The boolean for success/failure.
	/// </returns>
	public async Task<bool> UpdateMemberFeesDataAsync(UpdateMemberFeesDTO memberFeesData, string currentUserAlias)
	{
		var memberFeesDataDomain = mapper.Map<UpdateMemberFees>(memberFeesData);
		return await memberFeesService.UpdateMemberFeesDataAsync(memberFeesData: memberFeesDataDomain, currentUserAlias);
	}
}
