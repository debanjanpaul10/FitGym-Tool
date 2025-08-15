// *********************************************************************************
//	<copyright file="FitGymCommonService.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Fit Gym Common Data Service Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.AIEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;
using FitGymTool.Domain.DrivenPorts;
using FitGymTool.Domain.DrivingPorts;
using FitGymTool.Domain.Helpers;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Domain.UseCases;

/// <summary>
/// The Fit Gym Common Data Service Class.
/// </summary>
/// <param name="commonManager">The Fit Gym common manager Service.</param>
/// <param name="logger">The logger.</param>
/// <seealso cref="ICommonService" />
public class CommonService(ICommonDataManager commonManager, IAIServicesManager aiServicesManager, ILogger<CommonService> logger) : ICommonService
{
	/// <summary>
	/// Adds the new bug report data asynchronous.
	/// </summary>
	/// <param name="bugReportDataDomain">The bug report data domain.</param>
	/// <returns>
	/// The boolean for success/failure
	/// </returns>
	public async Task<bool> AddNewBugReportDataAsync(BugReportData bugReportDataDomain)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, bugReportDataDomain.CreatedBy));

			bugReportDataDomain.PrepareBugReportDataDomain();
			return await commonManager.AddNewBugReportDataAsync(bugReportDataDomain);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, bugReportDataDomain.CreatedBy));
		}
	}

	/// <summary>
	/// Gets the bug severity from ai service asynchronous.
	/// </summary>
	/// <param name="bugSeverityInput">The bug severity input.</param>
	/// <returns>
	/// The bug severity response.
	/// </returns>
	public async Task<BugSeverityResponse> GetBugSeverityFromAIServiceAsync(BugSeverityInput bugSeverityInput)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetBugSeverityFromAIServiceAsync), DateTime.UtcNow, bugSeverityInput.BugTitle));
			return await aiServicesManager.GetBugSeverityFromAIServiceAsync(bugSeverityInput).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetBugSeverityFromAIServiceAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetBugSeverityFromAIServiceAsync), DateTime.UtcNow, bugSeverityInput.BugTitle));
		}
	}

	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>
	/// The mapping master data dto.
	/// </returns>
	public async Task<MappingMasterData> GetMappingsMasterDataAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
			return await commonManager.GetMappingsMasterDataAsync();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
		}
	}
}
