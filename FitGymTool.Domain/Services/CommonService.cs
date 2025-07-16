// *********************************************************************************
//	<copyright file="FitGymCommonService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fit Gym Common Data Service Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DrivenPorts;
using FitGymTool.Domain.DrivingPorts;
using FitGymTool.Domain.Models;
using FitGymTool.Domain.Models.MappingDomain;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Domain.Services;

/// <summary>
/// The Fit Gym Common Data Service Class.
/// </summary>
/// <param name="fitGymDataService">The Fit Gym common manager Service.</param>
/// <param name="mapper">The mapper.</param>
/// <param name="logger">The logger.</param>
/// <seealso cref="DrivingPorts.ICommonService" />
public class CommonService(ICommonDataManager fitGymDataService, ILogger<CommonService> logger) : ICommonService
{
	/// <summary>
	/// The figym common manager.
	/// </summary>
	private readonly ICommonDataManager _commonManager = fitGymDataService;

	/// <summary>
	/// The logger.
	/// </summary>
	private readonly ILogger<CommonService> _logger = logger;

	/// <summary>
	/// Adds the new bug report data asynchronous.
	/// </summary>
	/// <param name="bugReportDataDomain">The bug report data domain.</param>
	/// <returns>
	/// The boolean for success/failure
	/// </returns>
	public async Task<bool> AddNewBugReportDataAsync(BugReportDataDomain bugReportDataDomain)
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, bugReportDataDomain.CreatedBy));
			
			bugReportDataDomain.IsActive = true;
			bugReportDataDomain.DateCreated = DateTime.UtcNow;
			bugReportDataDomain.DateModified = DateTime.UtcNow;
			bugReportDataDomain.ModifiedBy = bugReportDataDomain.CreatedBy;

			return await this._commonManager.AddNewBugReportDataAsync(bugReportDataDomain);
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, bugReportDataDomain.CreatedBy));
		}
	}

	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>
	/// The mapping master data dto.
	/// </returns>
	public async Task<MappingMasterDataDomain> GetMappingsMasterDataAsync()
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
			return await this._commonManager.GetMappingsMasterDataAsync();
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
		}
	}
}
