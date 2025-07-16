// *********************************************************************************
//	<copyright file="FitGymCommonDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The FitGym Common Data Service Class.</summary>
// *********************************************************************************

using FitGymTool.DataAccess.Contracts;
using FitGymTool.DataAccess.Entity.Mapping;
using FitGymTool.Shared.Constants;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace FitGymTool.DataAccess.Services;

/// <summary>
/// The FitGym Common Data Service Class.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="unitOfWork">The unit of work.</param>
/// <seealso cref="FitGymTool.DataAccess.Contracts.IFitGymCommonDataService" />
public class FitGymCommonDataService(IUnitOfWork unitOfWork, ILogger<FitGymCommonDataService> logger) : IFitGymCommonDataService
{
	/// <summary>
	/// The unit of work
	/// </summary>
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	/// <summary>
	/// The logger service.
	/// </summary>
	private readonly ILogger<FitGymCommonDataService> _logger = logger;

	/// <summary>
	/// Gets the mappings master data asynchronous.
	/// </summary>
	/// <returns>A tupple containing the mapping master data.</returns>
	public async Task<(List<FeesDurationMapping>, List<FeesPaymentStatusMapping>, List<MembershipStatusMapping>)> GetMappingsMasterDataAsync()
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(GetMappingsMasterDataAsync), DateTime.UtcNow, FitGymToolConstants.NotApplicableStringConstant));
			
			var feesDurationMapping = await this._unitOfWork.Repository<FeesDurationMapping>().GetAllAsync(filter: x => x.IsActive);
			var feesPaymentStatusMapping = await this._unitOfWork.Repository<FeesPaymentStatusMapping>().GetAllAsync(filter: x => x.IsActive);
			var membershipStatusMapping = await this._unitOfWork.Repository<MembershipStatusMapping>().GetAllAsync(filter: x => x.IsActive);


			return (feesDurationMapping, feesPaymentStatusMapping, membershipStatusMapping);

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
