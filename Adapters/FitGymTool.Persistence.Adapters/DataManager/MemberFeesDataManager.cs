// *********************************************************************************
//	<copyright file="MemberFeesDataManager.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Members Fees Data Service Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;
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
	public async Task<IEnumerable<CurrentMonthFeesAndRevenueStatus>> GetCurrentMonthFeesAndRevenueStatusAsync()
	{
		try
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));

			return await _unitOfWork.ExecuteSqlQueryAsync<CurrentMonthFeesAndRevenueStatus>(DatabaseConstants.SqlQueryExecutionConstants.Execute_FN_GetCurrentFeesAndRevenueStatus);
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

	/// <summary>
	/// Gets the current fees structure asynchronous.
	/// </summary>
	/// <returns>
	/// The list of <see cref="T:FitGymTool.Domain.Models.FeesStructureDomain" />
	/// </returns>
	public async Task<IEnumerable<FeesStructure>> GetCurrentFeesStructureAsync()
	{
		try
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetCurrentFeesStructureAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
			return await _unitOfWork.Repository<FeesStructure>().GetAllAsync(filter: fs => fs.IsActive, includeProperties: nameof(FeesStructure.FeesDurationMapping));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetCurrentFeesStructureAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetCurrentFeesStructureAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
		}
	}

	/// <summary>
	/// Gets the current members fees status asynchronous.
	/// </summary>
	/// <returns>
	/// The list of <see cref="CurrentMembersFeesStatus" />
	/// </returns>
	public async Task<IEnumerable<CurrentMembersFeesStatus>> GetCurrentMembersFeesStatusAsync()
	{
		try
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetCurrentMembersFeesStatusAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
			return await _unitOfWork.ExecuteSqlQueryAsync<CurrentMembersFeesStatus>(DatabaseConstants.SqlQueryExecutionConstants.Execute_FN_GetCurrentMembersFeesStatus);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetCurrentMembersFeesStatusAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetCurrentMembersFeesStatusAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
		}
	}

	public Task<IEnumerable<MemberPaymentHistoryData>> GetPaymentHistoryDataForMemberAsync(string userEmailId)
	{
		throw new NotImplementedException();
	}
}
