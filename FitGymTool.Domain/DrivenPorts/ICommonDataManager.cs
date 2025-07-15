// *********************************************************************************
//	<copyright file="IFitGymCommonDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The FitGym Common Data Service Interface.</summary>
// *********************************************************************************

using FitGymTool.Domain.Models.MappingDomain;

namespace FitGymTool.Domain.DrivenPorts;

/// <summary>
/// The FitGym Common Data Service Interface.
/// </summary>
public interface IFitGymCommonManager
{
	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>The mapping master data domain.</returns>
	Task<MappingMasterDataDomain> GetMappingsMasterDataAsync();
}
