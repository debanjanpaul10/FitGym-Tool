// *********************************************************************************
//	<copyright file="IMemberFeesDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Data Service Class.</summary>
// *********************************************************************************

using FitGymTool.DataAccess.Contracts;
using FitGymTool.DataAccess.Entity;

namespace FitGymTool.DataAccess.Services;

/// <summary>
/// The Members Data Service Class.
/// </summary>
/// <param name="dbContext">The SQL DB context.</param>
/// <seealso cref="FitGymTool.DataAccess.Contracts.IMemberFeesDataService" />
public class MemberFeesDataService(SqlDbContext dbContext) : IMemberFeesDataService
{
	/// <summary>
	/// The database context
	/// </summary>
	private readonly SqlDbContext _dbContext = dbContext;

	public Task<IEnumerable<FeesStatus>> GetCurrentFeesStatusForAllMembersAsync()
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<FeesPaymentHistory>> GetFeesPaymentHistoryForMemberAsync(string memberEmailAddress)
	{
		throw new NotImplementedException();
	}

	public Task<FeesStatus> GetFeesStatusForMemberAsync(string memberEmailAddress)
	{
		throw new NotImplementedException();
	}
}
