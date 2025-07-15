// *********************************************************************************
//	<copyright file="FitGymCommonService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fit Gym Common Data Service Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.Domain.DrivenPorts;
using FitGymTool.Domain.DrivingPorts;
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
/// <seealso cref="DrivingPorts.IFitGymCommonService" />
public class FitGymCommonService(IFitGymCommonManager fitGymDataService, IMapper mapper, ILogger<FitGymCommonService> logger) : IFitGymCommonService
{
	/// <summary>
	/// The figym common manager.
	/// </summary>
	private readonly IFitGymCommonManager _commonManager = fitGymDataService;

	/// <summary>
	/// The mapper.
	/// </summary>
	private readonly IMapper _mapper = mapper;

	/// <summary>
	/// The logger.
	/// </summary>
	private readonly ILogger<FitGymCommonService> _logger = logger;

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
			var (feesDurationMapping, feesPaymentStatusMapping, membershipStatusMapping) = await this._commonManager.GetMappingsMasterDataAsync();

			var feesDurationData = this._mapper.Map<List<FeesDurationMappingDomain>>(feesDurationMapping);
			var feesPaymentStatus = this._mapper.Map<List<FeesPaymentStatusMappingDomain>>(feesPaymentStatusMapping);
			var membershipStatus = this._mapper.Map<List<MembershipStatusMappingDomain>>(membershipStatusMapping);

			return new MappingMasterDataDomain()
			{
				FeesDurationMapping = feesDurationData,
				FeesPaymentStatusMapping = feesPaymentStatus,
				MembershipStatusMapping	= membershipStatus
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
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
		}
	}
}
