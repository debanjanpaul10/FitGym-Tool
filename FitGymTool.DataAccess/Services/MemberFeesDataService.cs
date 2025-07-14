// *********************************************************************************
//	<copyright file="IMemberFeesDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Data Service Class.</summary>
// *********************************************************************************

using FitGymTool.DataAccess.Contracts;
using FitGymTool.Shared.Constants;
using FitGymTool.Shared.Models;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace FitGymTool.DataAccess.Services;

/// <summary>
/// The Members Data Service Class.
/// </summary>
/// <param name="unitOfWork">The unit of work.</param>
/// <param name="logger">The logger.</param>
/// <seealso cref="FitGymTool.DataAccess.Contracts.IMemberFeesDataService" />
public class MemberFeesDataService(IUnitOfWork unitOfWork, ILogger<MemberFeesDataService> logger) : IMemberFeesDataService
{
	/// <summary>
	/// The unit of work
	/// </summary>
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	/// <summary>
	/// The logger
	/// </summary>
	private readonly ILogger<MemberFeesDataService> _logger = logger;

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

			var result = await _unitOfWork.ExecuteSqlQueryAsync<CurrentMonthFeesAndRevenueStatus>(DatabaseConstants.SqlQueryExecutionConstants.Execute_FN_GetCurrentFeesAndRevenueStatus);
			return result;
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
