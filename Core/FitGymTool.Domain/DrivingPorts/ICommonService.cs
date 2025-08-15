// *********************************************************************************
//	<copyright file="IFitGymCommonService.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Fit Gym Common Data Service Interface.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.AIEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;

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
	Task<MappingMasterData> GetMappingsMasterDataAsync();

	/// <summary>
	/// Adds the new bug report data asynchronous.
	/// </summary>
	/// <param name="bugReportData">The bug report data domain.</param>
	/// <returns>The boolean for success/failure</returns>
	Task<bool> AddNewBugReportDataAsync(BugReportData bugReportData);

	/// <summary>
	/// Gets the bug severity from ai service asynchronous.
	/// </summary>
	/// <param name="bugSeverityInput">The bug severity input.</param>
	/// <returns>The bug severity response.</returns>
	Task<BugSeverityResponse> GetBugSeverityFromAIServiceAsync(BugSeverityInput bugSeverityInput);

	/// <summary>
	/// Gets the active ai features asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="AIFeature"/></returns>
	Task<IEnumerable<AIFeature>> GetActiveAIFeaturesAsync();
}
