// *********************************************************************************
//	<copyright file="IMemberFeesDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Data Service Interface.</summary>
// *********************************************************************************

using FitGymTool.Domain.Models;

namespace FitGymTool.Domain.Ports.Out;

/// <summary>
/// The Member Fees Data Service Interface.
/// </summary>
public interface IMemberFeesDataManager
{
	/// <summary>
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>The list of current month fees and revenue status.</returns>
	Task<IEnumerable<CurrentMonthFeesAndRevenueStatusDomain>> GetCurrentMonthFeesAndRevenueStatusAsync();
}
