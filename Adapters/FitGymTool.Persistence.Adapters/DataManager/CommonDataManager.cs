// *********************************************************************************
//	<copyright file="FitGymCommonDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The FitGym Common Data Service Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.Domain.Models;
using FitGymTool.Domain.Models.MappingDomain;
using FitGymTool.Domain.Ports.Out;
using FitGymTool.Persistence.Adapters.Contracts;
using FitGymTool.Persistence.Adapters.Entity;
using FitGymTool.Persistence.Adapters.Entity.Mapping;
using FitGymTool.Persistence.Adapters.Helpers.Constants;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Persistence.Adapters.DataManager;

/// <summary>
/// The FitGym Common Data Service Class.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="unitOfWork">The unit of work.</param>
/// <param name="mapper">The auto mapper.</param>
/// <seealso cref="ICommonDataManager" />
public class CommonDataManager(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CommonDataManager> logger) : ICommonDataManager
{
	/// <summary>
	/// The unit of work
	/// </summary>
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	/// <summary>
	/// The mapper
	/// </summary>
	private readonly IMapper _mapper = mapper;

	/// <summary>
	/// The logger service.
	/// </summary>
	private readonly ILogger<CommonDataManager> _logger = logger;

	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>A tupple containing the mapping master data.</returns>
	public async Task<MappingMasterDataDomain> GetMappingsMasterDataAsync()
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, DatabaseConstants.NotApplicableStringConstant));

			var feesDurationMapping = await this._unitOfWork.Repository<FeesDurationMapping>().GetAllAsync(filter: x => x.IsActive);
			var feesPaymentStatusMapping = await this._unitOfWork.Repository<FeesPaymentStatusMapping>().GetAllAsync(filter: x => x.IsActive);
			var membershipStatusMapping = await this._unitOfWork.Repository<MembershipStatusMapping>().GetAllAsync(filter: x => x.IsActive);
			var bugSeverityMapping = await this._unitOfWork.Repository<BugSeverityMapping>().GetAllAsync(filter: x => x.IsActive);

			var feesDurationMappingDomain = this._mapper.Map<IEnumerable<FeesDurationMappingDomain>>(feesDurationMapping);
			var feesPaymentMappingDomain = this._mapper.Map<IEnumerable<FeesPaymentStatusMappingDomain>>(feesPaymentStatusMapping);
			var membershipStatusMappingDomain = this._mapper.Map<IEnumerable<MembershipStatusMappingDomain>>(membershipStatusMapping);
			var bugSeverityMappingDomain = this._mapper.Map<IEnumerable<BugSeverityMappingDomain>>(bugSeverityMapping);

			return new MappingMasterDataDomain()
			{
				FeesDurationMapping = feesDurationMappingDomain,
				FeesPaymentStatusMapping = feesPaymentMappingDomain,
				MembershipStatusMapping = membershipStatusMappingDomain,
				BugSeverityMapping = bugSeverityMappingDomain,
			};
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
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, DatabaseConstants.NotApplicableStringConstant));
		}
	}

	/// <summary>
	/// Adds the new bug report data asynchronous.
	/// </summary>
	/// <param name="bugReportDataDomain">The bug report data.</param>
	/// <returns>The boolean for success/failure.</returns>
	public async Task<bool> AddNewBugReportDataAsync(BugReportDataDomain bugReportDataDomain)
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, bugReportDataDomain.CreatedBy));
			var bugSeverityEntity = await this._unitOfWork.Repository<BugSeverityMapping>().FirstOrDefaultAsync(sev => sev.SeverityName == DatabaseConstants.MediumConstant && sev.IsActive);
			var bugStatusEntity = await this._unitOfWork.Repository<BugItemStatusMapping>().FirstOrDefaultAsync(status => status.StatusName == DatabaseConstants.NotStartedConstant && status.IsActive);

			var bugReportData = this._mapper.Map<BugReportData>(bugReportDataDomain);
			
			//TODO: overwriting as of now, later needs to be checked by AI or something.
			bugReportData.BugSeverityId = bugSeverityEntity?.Id ?? 0;
			bugReportData.BugStatusId = bugStatusEntity?.Id ?? 0;

			await this._unitOfWork.Repository<BugReportData>().AddAsync(bugReportData);
			await this._unitOfWork.SaveChangesAsync();

			return true;
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
}
