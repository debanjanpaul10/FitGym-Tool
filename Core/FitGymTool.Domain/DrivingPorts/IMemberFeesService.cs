// *********************************************************************************
//	<copyright file="IMemberFeesService.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Member Fees Service Interface.</summary>
// *********************************************************************************


using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;

namespace FitGymTool.Domain.DrivingPorts;

/// <summary>
/// The Member Fees Service Interface.
/// </summary>
public interface IMemberFeesService
{
	/// <summary>
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="CurrentMonthFeesAndRevenueStatus"/></returns>
	Task<IEnumerable<CurrentMonthFeesAndRevenueStatus>> GetCurrentMonthFeesAndRevenueStatusAsync();

	/// <summary>
	/// Gets the current fees structure asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="FeesStructure"/></returns>
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

	/// <summary>
	/// Updates the member fees data asynchronous.
	/// </summary>
	/// <param name="memberFeesData">The member fees data.</param>
	/// <param name="currentUserAlias">The current user alias.</param>
	/// <returns>The boolean for success/failure.</returns>
	Task<bool> UpdateMemberFeesDataAsync(UpdateMemberFees memberFeesData, string currentUserAlias);
}
