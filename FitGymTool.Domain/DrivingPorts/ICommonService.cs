// *********************************************************************************
//	<copyright file="IFitGymCommonService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fit Gym Common Data Service Interface.</summary>
// *********************************************************************************

using FitGymTool.Domain.Models;
using FitGymTool.Domain.Models.MappingDomain;

namespace FitGymTool.Domain.DrivingPorts;

/// <summary>
/// The Fit Gym Common Data Service Interface.
/// </summary>
public interface ICommonService
{
	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>
	/// The mapping master data dto.
	/// </returns>
	Task<MappingMasterDataDomain> GetMappingsMasterDataAsync();

	/// <summary>
	/// Adds the new bug report data asynchronous.
	/// </summary>
	/// <param name="bugReportDataDomain">The bug report data domain.</param>
	/// <returns>The boolean for success/failure</returns>
	Task<bool> AddNewBugReportDataAsync(BugReportDataDomain bugReportDataDomain);
}
