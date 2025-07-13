// *********************************************************************************
//	<copyright file="IMemberFeesDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Data Service Interface.</summary>
// *********************************************************************************

using FitGymTool.DataAccess.Entity;

namespace FitGymTool.DataAccess.Contracts;

/// <summary>
/// The Member Fees Data Service Interface.
/// </summary>
public interface IMemberFeesDataService
{
	Task<IEnumerable<FeesPaymentHistory>> GetFeesPaymentHistoryForMemberAsync(string memberEmailAddress);

	Task<FeesStatus> GetFeesStatusForMemberAsync(string memberEmailAddress);

	Task<IEnumerable<FeesStatus>> GetCurrentFeesStatusForAllMembersAsync();
}
