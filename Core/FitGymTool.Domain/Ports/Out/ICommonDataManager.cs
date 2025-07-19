// *********************************************************************************
//	<copyright file="IFitGymCommonDataService.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The FitGym Common Data Service Interface.</summary>
// *********************************************************************************

using FitGymTool.Domain.Models;
using FitGymTool.Domain.Models.MappingDomain;

namespace FitGymTool.Domain.Ports.Out;

/// <summary>
/// The FitGym Common Data Service Interface.
/// </summary>
public interface ICommonDataManager
{
	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>The mapping master data domain.</returns>
	Task<MappingMasterDataDomain> GetMappingsMasterDataAsync();

	/// <summary>
	/// Adds the new bug report data asynchronous.
	/// </summary>
	/// <param name="bugReportDataDomain">The bug report data.</param>
	/// <returns>The boolean for success/failure.</returns>
	Task<bool> AddNewBugReportDataAsync(BugReportDataDomain bugReportDataDomain);
}
