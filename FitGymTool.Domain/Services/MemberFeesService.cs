// *********************************************************************************
//	<copyright file="MemberFeesService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Member Fees Service Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.Models;
using FitGymTool.Domain.Ports.In;
using FitGymTool.Domain.Ports.Out;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Domain.Services;

/// <summary>
/// The Member Fees Service Class.
/// </summary>
/// <param name="logger">The logger.</param>
/// <param name="memberFeesDataService">The member fees data service.</param>
/// <seealso cref="Ports.In.IMemberFeesService" />
public class MemberFeesService(IMemberFeesDataManager memberFeesDataService, ILogger<MemberFeesService> logger) : IMemberFeesService
{
	/// <summary>
	/// The member fees data service.
	/// </summary>
	private readonly IMemberFeesDataManager _memberFeesDataService = memberFeesDataService;

	/// <summary>
	/// The logger/
	/// </summary>
	private readonly ILogger<MemberFeesService> _logger = logger;

	/// <summary>
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>The list of current month fees and revenue status.</returns>
	public async Task<IEnumerable<CurrentMonthFeesAndRevenueStatusDomain>> GetCurrentMonthFeesAndRevenueStatusAsync()
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
			
			return await this._memberFeesDataService.GetCurrentMonthFeesAndRevenueStatusAsync();
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
		}
	}
}
