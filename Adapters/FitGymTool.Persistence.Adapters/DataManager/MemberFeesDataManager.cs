﻿// *********************************************************************************
//	<copyright file="IMemberFeesDataService.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Members Data Service Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.Models;
using FitGymTool.Domain.Ports.Out;
using FitGymTool.Persistence.Adapters.Contracts;
using FitGymTool.Persistence.Adapters.Helpers.Constants;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Persistence.Adapters.DataManager;

/// <summary>
/// The Members Data Service Class.
/// </summary>
/// <param name="unitOfWork">The unit of work.</param>
/// <param name="logger">The logger.</param>
/// <seealso cref="IMemberFeesDataManager" />
public class MemberFeesDataManager(IUnitOfWork unitOfWork, ILogger<MemberFeesDataManager> logger) : IMemberFeesDataManager
{
	/// <summary>
	/// The unit of work
	/// </summary>
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	/// <summary>
	/// The logger
	/// </summary>
	private readonly ILogger<MemberFeesDataManager> _logger = logger;

	/// <summary>
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>The list of current month fees and revenue status.</returns>
	public async Task<IEnumerable<CurrentMonthFeesAndRevenueStatusDomain>> GetCurrentMonthFeesAndRevenueStatusAsync()
	{
		try
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));

			var result = await _unitOfWork.ExecuteSqlQueryAsync<CurrentMonthFeesAndRevenueStatusDomain>(DatabaseConstants.SqlQueryExecutionConstants.Execute_FN_GetCurrentFeesAndRevenueStatus);
			return result;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
		}
	}
}
