// *********************************************************************************
//	<copyright file="IMemberFeesService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Member Fees Service Interface.</summary>
// *********************************************************************************

using FitGymTool.Shared.Models;

namespace FitGymTool.Business.Contracts;

/// <summary>
/// The Member Fees Service Interface.
/// </summary>
public interface IMemberFeesService
{
	/// <summary>
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>The list of current month fees and revenue status.</returns>
	Task<IEnumerable<CurrentMonthFeesAndRevenueStatus>> GetCurrentMonthFeesAndRevenueStatusAsync();
}
