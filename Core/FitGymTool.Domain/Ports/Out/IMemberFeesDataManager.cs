// *********************************************************************************
//	<copyright file="IMemberFeesDataService.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Members Data Service Interface.</summary>
// *********************************************************************************


using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;

namespace FitGymTool.Domain.Ports.Out;

/// <summary>
/// The Member Fees Data Service Interface.
/// </summary>
public interface IMemberFeesDataManager
{
	/// <summary>
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="CurrentMonthFeesAndRevenueStatusDomain"/>.</returns>
	Task<IEnumerable<CurrentMonthFeesAndRevenueStatus>> GetCurrentMonthFeesAndRevenueStatusAsync();

	/// <summary>
	/// Gets the current fees structure asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="FeesStructureDomain"/></returns>
	Task<IEnumerable<FeesStructure>> GetCurrentFeesStructureAsync();

	/// <summary>
	/// Gets the current members fees status asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="CurrentMembersFeesStatus"/></returns>
	Task<IEnumerable<CurrentMembersFeesStatus>> GetCurrentMembersFeesStatusAsync();

	/// <summary>
	/// Gets the payment history data for member asynchronous.
	/// </summary>
	/// <param name="userEmailId">The user email address.</param>
	/// <returns>The list of <see cref="MemberPaymentHistoryData"/></returns>
	Task<IEnumerable<MemberPaymentHistoryData>> GetPaymentHistoryDataForMemberAsync(string userEmailId);
}
