// *********************************************************************************
//	<copyright file="CommonDataManager.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The FitGym Common Data Manager Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;
using FitGymTool.Domain.DomainEntities.Mapping;
using FitGymTool.Domain.Ports.Out;
using FitGymTool.Persistence.Adapters.Contracts;
using FitGymTool.Persistence.Adapters.Helpers.Constants;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Persistence.Adapters.DataManager;

/// <summary>
/// The FitGym Common Data Manager Class.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="unitOfWork">The unit of work.</param>
/// <seealso cref="ICommonDataManager" />
public class CommonDataManager(IUnitOfWork unitOfWork, ILogger<CommonDataManager> logger) : ICommonDataManager
{
	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>A tupple containing the mapping master data.</returns>
	public async Task<MappingMasterData> GetMappingsMasterDataAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, DatabaseConstants.NotApplicableStringConstant));

			var feesDurationMapping = await unitOfWork.Repository<FeesDurationMapping>().GetAllAsync(filter: x => x.IsActive);
			var feesPaymentStatusMapping = await unitOfWork.Repository<FeesPaymentStatusMapping>().GetAllAsync(filter: x => x.IsActive);
			var membershipStatusMapping = await unitOfWork.Repository<MembershipStatusMapping>().GetAllAsync(filter: x => x.IsActive);
			var bugSeverityMapping = await unitOfWork.Repository<BugSeverityMapping>().GetAllAsync(filter: x => x.IsActive);


			return new MappingMasterData()
			{
				FeesDurationMapping = feesDurationMapping,
				FeesPaymentStatusMapping = feesPaymentStatusMapping,
				MembershipStatusMapping = membershipStatusMapping,
				BugSeverityMapping = bugSeverityMapping,
			};
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, DatabaseConstants.NotApplicableStringConstant));
		}
	}

	/// <summary>
	/// Adds the new bug report data asynchronous.
	/// </summary>
	/// <param name="bugReportData">The bug report data.</param>
	/// <returns>The boolean for success/failure.</returns>
	public async Task<bool> AddNewBugReportDataAsync(BugReportData bugReportData)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, bugReportData.CreatedBy));
			
			var bugSeverityEntity = await unitOfWork.Repository<BugSeverityMapping>().FirstOrDefaultAsync(sev => sev.SeverityName == DatabaseConstants.MediumConstant && sev.IsActive);
			var bugStatusEntity = await unitOfWork.Repository<BugItemStatusMapping>().FirstOrDefaultAsync(status => status.StatusName == DatabaseConstants.NotStartedConstant && status.IsActive);

			//TODO: overwriting as of now, later needs to be checked by AI or something.
			bugReportData.BugSeverityId = bugSeverityEntity?.Id ?? 0;
			bugReportData.BugStatusId = bugStatusEntity?.Id ?? 0;

			await unitOfWork.Repository<BugReportData>().AddAsync(bugReportData);
			await unitOfWork.SaveChangesAsync();

			return true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(AddNewBugReportDataAsync), DateTime.UtcNow, bugReportData.CreatedBy));
		}
	}
}
