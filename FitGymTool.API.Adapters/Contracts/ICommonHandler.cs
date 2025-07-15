// *********************************************************************************
//	<copyright file="IFitGymCommonHandler.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fit Gym Common Handler Adapter Interface.</summary>
// *********************************************************************************

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
}
