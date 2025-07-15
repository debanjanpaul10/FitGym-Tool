// *********************************************************************************
//	<copyright file="FitGymCommonDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The FitGym Common Data Service Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.Domain.DrivenPorts;
using FitGymTool.Domain.Models.MappingDomain;
using FitGymTool.Infrastructure.DB.Contracts;
using FitGymTool.Infrastructure.DB.Entity.Mapping;
using FitGymTool.Infrastructure.DB.Helpers.Constants;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Infrastructure.DB.DataManager;

/// <summary>
/// The FitGym Common Data Service Class.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="unitOfWork">The unit of work.</param>
/// <seealso cref="IFitGymCommonManager" />
public class FitGymCommonDataService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FitGymCommonDataService> logger) : IFitGymCommonManager
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
	private readonly ILogger<FitGymCommonDataService> _logger = logger;

	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>A tupple containing the mapping master data.</returns>
	public async Task<(List<FeesDurationMappingDomain>, List<FeesPaymentStatusMappingDomain>, List<MembershipStatusMappingDomain>)> GetMappingsMasterDataAsync()
	{
		try
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, DatabaseConstants.NotApplicableStringConstant));

			var feesDurationMapping = await _unitOfWork.Repository<FeesDurationMapping>().GetAllAsync(filter: x => x.IsActive);
			var feesPaymentStatusMapping = await _unitOfWork.Repository<FeesPaymentStatusMapping>().GetAllAsync(filter: x => x.IsActive);
			var membershipStatusMapping = await _unitOfWork.Repository<MembershipStatusMapping>().GetAllAsync(filter: x => x.IsActive);

			var feesDurationMappingDomain = _mapper.Map<List<FeesDurationMappingDomain>>(feesDurationMapping);
			var feesPaymentMappingDomain = _mapper.Map<List<FeesPaymentStatusMappingDomain>>(feesPaymentStatusMapping);
			var membershipStatusMappingDomain = _mapper.Map<List<MembershipStatusMappingDomain>>(membershipStatusMapping);

			return (feesDurationMappingDomain, feesPaymentMappingDomain, membershipStatusMappingDomain);

		}
		catch (Exception ex)
		{
			_logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, DatabaseConstants.NotApplicableStringConstant));
		}
	}
}
