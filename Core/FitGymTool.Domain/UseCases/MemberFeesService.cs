// *********************************************************************************
//	<copyright file="MemberFeesService.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Member Fees Service Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;
using FitGymTool.Domain.DrivenPorts;
using FitGymTool.Domain.DrivingPorts;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Domain.UseCases;

/// <summary>
/// The Member Fees Service Class.
/// </summary>
/// <param name="logger">The logger.</param>
/// <param name="memberFeesDataService">The member fees data service.</param>
/// <seealso cref="IMemberFeesService" />
public class MemberFeesService(IMemberFeesDataManager memberFeesDataService, ILogger<MemberFeesService> logger) : IMemberFeesService
{
	/// <summary>
	/// Gets the current fees structure asynchronous.
	/// </summary>
	/// <returns>
	/// The list of <see cref="FeesStructure" />
	/// </returns>
	public async Task<IEnumerable<FeesStructure>> GetCurrentFeesStructureAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetCurrentFeesStructureAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
			return await memberFeesDataService.GetCurrentFeesStructureAsync();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetCurrentFeesStructureAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetCurrentFeesStructureAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
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
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetCurrentMembersFeesStatusAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
			return await memberFeesDataService.GetCurrentMembersFeesStatusAsync();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetCurrentMembersFeesStatusAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetCurrentMembersFeesStatusAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
		}
	}

	/// <summary>
	/// Gets the current month fees and revenue status asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="CurrentMonthFeesAndRevenueStatus"/></returns>
	public async Task<IEnumerable<CurrentMonthFeesAndRevenueStatus>> GetCurrentMonthFeesAndRevenueStatusAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
			
			return await memberFeesDataService.GetCurrentMonthFeesAndRevenueStatusAsync();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetCurrentMonthFeesAndRevenueStatusAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
		}
	}

	/// <summary>
	/// Gets the payment history data for member asynchronous.
	/// </summary>
	/// <param name="userEmailId">The user email address.</param>
	/// <returns>
	/// The list of <see cref="MemberPaymentHistoryData" />
	/// </returns>
	public async Task<IEnumerable<MemberPaymentHistoryData>> GetPaymentHistoryDataForMemberAsync(string userEmailId)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetPaymentHistoryDataForMemberAsync), DateTime.UtcNow, userEmailId));

			return await memberFeesDataService.GetPaymentHistoryDataForMemberAsync(userEmailId);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetPaymentHistoryDataForMemberAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetPaymentHistoryDataForMemberAsync), DateTime.UtcNow, userEmailId));
		}
	}

	/// <summary>
	/// Updates the member fees data asynchronous.
	/// </summary>
	/// <param name="memberFeesData">The member fees data.</param>
	/// <param name="currentUserAlias">The current user alias.</param>
	/// <returns>
	/// The boolean for success/failure.
	/// </returns>
	public async Task<bool> UpdateMemberFeesDataAsync(UpdateMemberFees memberFeesData, string currentUserAlias)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(UpdateMemberFeesDataAsync), DateTime.UtcNow, memberFeesData.MemberEmailAddress));
			memberFeesData.ModifiedBy = currentUserAlias;
			return await memberFeesDataService.UpdateMemberFeesDataAsync(memberFeesData);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMemberFeesDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(UpdateMemberFeesDataAsync), DateTime.UtcNow, memberFeesData.MemberEmailAddress));
		}
	}
}
