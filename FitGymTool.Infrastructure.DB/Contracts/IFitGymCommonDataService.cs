// *********************************************************************************
//	<copyright file="IFitGymCommonDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The FitGym Common Data Service Interface.</summary>
// *********************************************************************************

using FitGymTool.Infrastructure.DB.Entity.Mapping;

namespace FitGymTool.Infrastructure.DB.Contracts;

/// <summary>
/// The FitGym Common Data Service Interface.
/// </summary>
public interface IFitGymCommonDataService
{
	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>A tupple containing the mapping master data.</returns>
	Task<(List<FeesDurationMapping>, List<FeesPaymentStatusMapping>, List<MembershipStatusMapping>)> GetMappingsMasterDataAsync();
}
