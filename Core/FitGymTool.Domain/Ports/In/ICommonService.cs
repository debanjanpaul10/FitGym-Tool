// *********************************************************************************
//	<copyright file="IFitGymCommonService.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Fit Gym Common Data Service Interface.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;

namespace FitGymTool.Domain.Ports.In;

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
	Task<MappingMasterData> GetMappingsMasterDataAsync();

	/// <summary>
	/// Adds the new bug report data asynchronous.
	/// </summary>
	/// <param name="bugReportData">The bug report data domain.</param>
	/// <returns>The boolean for success/failure</returns>
	Task<bool> AddNewBugReportDataAsync(BugReportData bugReportData);
}
