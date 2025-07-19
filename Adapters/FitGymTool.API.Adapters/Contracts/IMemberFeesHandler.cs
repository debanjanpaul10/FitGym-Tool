// *********************************************************************************
//	<copyright file="IMemberFeesHandler.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Member Fees Handler Adapter Interface.</summary>
// *********************************************************************************

using FitGymTool.API.Adapters.Models.Response;

namespace FitGymTool.API.Adapters.Contracts;

/// <summary>
/// The Member Fees Handler Adapter Interface.
/// </summary>
public interface IMemberFeesHandler
{
	/// <summary>
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>The list of current month fees and revenue status.</returns>
	Task<IEnumerable<CurrentMonthFeesAndRevenueStatusDto>> GetCurrentMonthFeesAndRevenueStatusAsync();
}
