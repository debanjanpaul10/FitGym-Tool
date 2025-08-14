// *********************************************************************************
//	<copyright file="MembersDataManager.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Members Data Manager Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;
using FitGymTool.Domain.DrivenPorts;
using FitGymTool.Domain.Helpers;
using FitGymTool.Persistence.Adapters.Contracts;
using FitGymTool.Persistence.Adapters.Helpers.Constants;
using FitGymTool.Persistence.Adapters.Helpers.Extensions;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static FitGymTool.Domain.Helpers.DomainConstants;
using static FitGymTool.Persistence.Adapters.Helpers.Extensions.PersistenceUtilities;

namespace FitGymTool.Persistence.Adapters.DataManager;

/// <summary>
/// The Members Data Manager Class.
/// </summary>
/// <param name="unitOfWork">The unit of work.</param>
/// <param name="logger">The logger.</param>
/// <seealso cref="IMembersDataManager"/>
public class MembersDataManager(IUnitOfWork unitOfWork, ILogger<MembersDataManager> logger) : IMembersDataManager
{

	/// <summary>
	/// Adds a new member to the database asynchronously.
	/// </summary>
	/// <param name="memberDetails">The member details data.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> AddNewMemberAsync(NewMemberDetails memberDetails)
	{
		try
		{
			memberDetails.EnsureValidDates();
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, memberDetails.MemberEmail));

			var parameters = memberDetails.PrepareNewMemberSPParameters();
			await unitOfWork.ExecuteSqlQueryAsync<object>(DatabaseConstants.StoredProceduresConstants.StoredProcedure_Names.AddNewMemberData_SP, parameters);
			return true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, memberDetails.MemberEmail));
		}
	}

	/// <summary>
	/// Gets all members from the database asynchronously.
	/// </summary>
	/// <returns>A list of MemberDetails.</returns>
	public async Task<List<MemberDetails>> GetAllMembersAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));

			return await unitOfWork.Repository<MemberDetails>().GetAllAsync(filter: m => m.IsActive, includeProperties: nameof(MemberDetails.MembershipStatusMapping));
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
		}
	}

	/// <summary>
	/// Gets a single member's details by Member's Email ID asynchronously.
	/// </summary>
	/// <param name="memberEmail">The member's Email ID.</param>
	/// <returns>The MemberDetails object if found; otherwise, null.</returns>
	public async Task<MemberDetails?> GetMemberByEmailIdAsync(string memberEmail)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, memberEmail));

			return await unitOfWork.Repository<MemberDetails>().GetAsync(
				filter: m => m.MemberEmail == memberEmail && m.IsActive, tracked: true, includeProperties: nameof(MemberDetails.MembershipStatusMapping));

		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, memberEmail));
		}
	}

	/// <summary>
	/// Updates an existing member's details asynchronously.
	/// </summary>
	/// <param name="memberDetails">The updated member details.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> UpdateMemberDetailsAsync(MemberDetails memberDetails)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(UpdateMemberDetailsAsync), DateTime.UtcNow, memberDetails.MemberEmail));

			var existingMember = await unitOfWork.Repository<MemberDetails>().FirstOrDefaultAsync(predicate: m => m.MemberId == memberDetails.MemberId && m.IsActive);
			if (existingMember is null)
			{
				var ex = new InvalidOperationException(ValidationErrorMessages.MemberNotFoundMessageConstant);
				logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMemberDetailsAsync), DateTime.UtcNow, ex.Message));
				throw ex;
			}

			// Update the entity for only updated values
			existingMember.PrepareUpdateMemberDataEntity(memberDetails);

			unitOfWork.Repository<MemberDetails>().Update(existingMember);
			await unitOfWork.SaveChangesAsync();

			return true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMemberDetailsAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(UpdateMemberDetailsAsync), DateTime.UtcNow, memberDetails.MemberEmail));
		}
	}

	/// <summary>
	/// Updates the membership status asynchronous.
	/// </summary>
	/// <param name="updateMembershipStatusData">The update membership status domain.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> UpdateMembershipStatusAsync(MemberDetails updateMembershipStatusData)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(UpdateMembershipStatusAsync), DateTime.UtcNow, updateMembershipStatusData.MemberEmail));
			var existingMember = await unitOfWork.Repository<MemberDetails>()
				.FirstOrDefaultAsync(predicate: member => member.MemberId == updateMembershipStatusData.MemberId && member.MemberEmail == updateMembershipStatusData.MemberEmail && member.IsActive);
			if (existingMember is null)
			{
				var ex = new InvalidOperationException(ValidationErrorMessages.MemberNotFoundMessageConstant);
				logger.LogError(ex, string.Format(
					CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMembershipStatusAsync), DateTime.UtcNow, ex.Message));
				throw ex;
			}

			existingMember.PrepareMembershipStatusUpdateDataEntity(updateMembershipStatusData);
			unitOfWork.Repository<MemberDetails>().Update(existingMember);
			await unitOfWork.SaveChangesAsync();

			return true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMembershipStatusAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(UpdateMembershipStatusAsync), DateTime.UtcNow, updateMembershipStatusData.MemberEmail));
		}
	}
}
