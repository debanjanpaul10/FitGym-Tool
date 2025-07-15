// *********************************************************************************
//	<copyright file="FitGymCommonService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fit Gym Common Data Service Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.Domain.Contracts;
using FitGymTool.Infrastructure.DB.Contracts;
using FitGymTool.Shared.Constants;
using FitGymTool.Shared.DTOs.MappingData;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace FitGymTool.Domain.Services;

/// <summary>
/// The Fit Gym Common Data Service Class.
/// </summary>
/// <param name="fitGymDataService">The Fit Gym Data Service.</param>
/// <param name="mapper">The mapper.</param>
/// <param name="logger">The logger.</param>
/// <seealso cref="FitGymTool.Domain.Contracts.IFitGymCommonService" />
public class FitGymCommonService(IFitGymCommonDataService fitGymDataService, IMapper mapper, ILogger<FitGymCommonService> logger) : IFitGymCommonService
{
	/// <summary>
	/// The figym data service.
	/// </summary>
	private readonly IFitGymCommonDataService _figymDataService = fitGymDataService;

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
	public async Task<MappingMasterDataDto> GetMappingsMasterDataAsync()
	{
		try
		{

			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, FitGymToolConstants.NotApplicableStringConstant));
			var (feesDurationMapping, feesPaymentStatusMapping, membershipStatusMapping) = await this._figymDataService.GetMappingsMasterDataAsync();

			var feesDurationData = this._mapper.Map<List<FeesDurationMappingDto>>(feesDurationMapping);
			var feesPaymentStatus = this._mapper.Map<List<FeesPaymentStatusMappingDto>>(feesPaymentStatusMapping);
			var membershipStatus = this._mapper.Map<List<MembershipStatusMappingDto>>(membershipStatusMapping);

			return new MappingMasterDataDto()
			{
				FeesDurationMapping = feesDurationData,
				FeesPaymentStatusMapping = feesPaymentStatus,
				MembershipStatusMapping	= membershipStatus
			};
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodEndedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, FitGymToolConstants.NotApplicableStringConstant));
		}
	}
}
