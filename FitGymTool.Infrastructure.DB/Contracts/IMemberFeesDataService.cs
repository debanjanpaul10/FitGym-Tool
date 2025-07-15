// *********************************************************************************
//	<copyright file="IMemberFeesDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Data Service Interface.</summary>
// *********************************************************************************

using FitGymTool.Shared.Models;

namespace FitGymTool.Infrastructure.DB.Contracts;

/// <summary>
/// The Member Fees Data Service Interface.
/// </summary>
public interface IMemberFeesDataService
{
	/// <summary>
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>The list of current month fees and revenue status.</returns>
	Task<IEnumerable<CurrentMonthFeesAndRevenueStatus>> GetCurrentMonthFeesAndRevenueStatusAsync();
}
