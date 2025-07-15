// *********************************************************************************
//	<copyright file="IFitGymCommonService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fit Gym Common Data Service Interface.</summary>
// *********************************************************************************

using FitGymTool.Shared.DTOs.MappingData;

namespace FitGymTool.Domain.Contracts;

/// <summary>
/// The Fit Gym Common Data Service Interface.
/// </summary>
public interface IFitGymCommonService
{
	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>
	/// The mapping master data dto.
	/// </returns>
	Task<MappingMasterDataDto> GetMappingsMasterDataAsync();
}
