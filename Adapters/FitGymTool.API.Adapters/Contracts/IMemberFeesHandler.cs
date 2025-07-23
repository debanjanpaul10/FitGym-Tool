// *********************************************************************************
//	<copyright file="IMemberFeesHandler.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Member Fees Handler Adapter Interface.</summary>
// *********************************************************************************

using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Adapters.Models.Response.DerivedEntities;

namespace FitGymTool.API.Adapters.Contracts;

/// <summary>
/// The Member Fees Handler Adapter Interface.
/// </summary>
public interface IMemberFeesHandler
{
	/// <summary>
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="CurrentMonthFeesAndRevenueStatusDTO"/></returns>
	Task<IEnumerable<CurrentMonthFeesAndRevenueStatusDTO>> GetCurrentMonthFeesAndRevenueStatusAsync();

	/// <summary>
	/// Gets the current fees structure asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="FeesStructureDTO"/></returns>
	Task<IEnumerable<FeesStructureDTO>> GetCurrentFeesStructureAsync();

	/// <summary>
	/// Gets the current members fees status asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="CurrentMembersFeesStatusDTO"/></returns>
	Task<IEnumerable<CurrentMembersFeesStatusDTO>> GetCurrentMembersFeesStatusAsync();

	/// <summary>
	/// Gets the payment history data for member asynchronous.
	/// </summary>
	/// <param name="userEmailId">The user email address.</param>
	/// <returns>The list of <see cref="MemberPaymentHistoryData"/></returns>
	Task<IEnumerable<MemberPaymentHistoryDTO>> GetPaymentHistoryDataForMemberAsync(string userEmailId);
}
