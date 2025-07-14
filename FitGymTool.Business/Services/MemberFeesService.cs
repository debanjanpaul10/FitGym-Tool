// *********************************************************************************
//	<copyright file="MemberFeesService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Member Fees Service Class.</summary>
// *********************************************************************************

using FitGymTool.Business.Contracts;
using FitGymTool.DataAccess.Contracts;
using FitGymTool.Shared.Constants;
using FitGymTool.Shared.Models;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace FitGymTool.Business.Services;

/// <summary>
/// The Member Fees Service Class.
/// </summary>
/// <param name="logger">The logger.</param>
/// <param name="memberFeesDataService">The member fees data service.</param>
/// <seealso cref="FitGymTool.Business.Contracts.IMemberFeesService" />
public class MemberFeesService(IMemberFeesDataService memberFeesDataService, ILogger<MemberFeesService> logger) : IMemberFeesService
{
	/// <summary>
	/// The member fees data service.
	/// </summary>
	private readonly IMemberFeesDataService _memberFeesDataService = memberFeesDataService;

	/// <summary>
	/// The logger/
	/// </summary>
	private readonly ILogger<MemberFeesService> _logger = logger;

	/// <summary>
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>The list of current month fees and revenue status.</returns>
	public async Task<IEnumerable<CurrentMonthFeesAndRevenueStatus>> GetCurrentMonthFeesAndRevenueStatusAsync()
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, FitGymToolConstants.NotApplicableStringConstant));
			
			return await this._memberFeesDataService.GetCurrentMonthFeesAndRevenueStatusAsync();
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodEndedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, FitGymToolConstants.NotApplicableStringConstant));
		}
	}
}
