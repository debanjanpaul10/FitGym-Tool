// *********************************************************************************
//	<copyright file="IFitGymCommonHandler.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Fit Gym Common Handler Adapter Interface.</summary>
// *********************************************************************************

using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.API.Adapters.Models.Response.MappingData;

namespace FitGymTool.API.Adapters.Contracts;

/// <summary>
/// The Fit Gym Common Handler Adapter Interface.
/// </summary>
public interface ICommonHandler
{
	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>
	/// The mapping master data dto.
	/// </returns>
	Task<MappingMasterDataDto> GetMappingsMasterDataAsync();

	/// <summary>
	/// Adds the new bug report data asynchronous.
	/// </summary>
	/// <param name="bugReportDataDTO">The bug report data domain.</param>
	/// <returns>The boolean for success/failure</returns>
	Task<bool> AddNewBugReportDataAsync(AddBugReportDTO bugReportDataDTO);

	/// <summary>
	/// Gets the bug severity from ai service asynchronous.
	/// </summary>
	/// <param name="bugSeverityInput">The bug severity input.</param>
	/// <returns>The bug severity response.</returns>
	Task<BugSeverityResponseDTO> GetBugSeverityFromAIServiceAsync(BugSeverityInputDTO bugSeverityInput);

	/// <summary>
	/// Gets the active ai features asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="AIFeaturesDTO"/></returns>
	Task<IEnumerable<AIFeaturesDTO>> GetActiveAIFeaturesAsync();
}
