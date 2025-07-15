// *********************************************************************************
//	<copyright file="MembersDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Data Service Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DrivenPorts;
using FitGymTool.Infrastructure.DB.Contracts;
using FitGymTool.Infrastructure.DB.Entity;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Infrastructure.DB.DataManager;

/// <summary>
/// The Members Data Service Class.
/// </summary>
/// <param name="unitOfWork">The unit of work.</param>
/// <param name="logger">The logger.</param>
/// <seealso cref="IMembersManager"/>
public class MembersDataService(IUnitOfWork unitOfWork, ILogger<MembersDataService> logger) : IMembersManager
{
	/// <summary>
	/// The unit of work for database operations.
	/// </summary>
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	/// <summary>
	/// The logger for logging operations.
	/// </summary>
	private readonly ILogger<MembersDataService> _logger = logger;

	/// <summary>
	/// Adds a new member to the database asynchronously.
	/// </summary>
	/// <param name="memberDetails">The member details data.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> AddNewMemberAsync(MemberDetails memberDetails)
	{
		try
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, memberDetails.MemberEmail));

			var existingMember = (await _unitOfWork.Repository<MemberDetails>()
			    .FindAsync(predicate: member => member.MemberEmail == memberDetails.MemberEmail && member.IsActive)).Any();
			if (existingMember)
			{
				var ex = new InvalidOperationException(ExceptionConstants.ValidationErrorMessages.MemberAlreadyExistsMessageConstant);
				_logger.LogError(ex, string.Format(
					CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, ex.Message));
				throw ex;
			}

			await _unitOfWork.Repository<MemberDetails>().AddAsync(memberDetails);
			await _unitOfWork.SaveChangesAsync();

			return true;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodEndedMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, memberDetails.MemberEmail));
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
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, FitGymToolConstants.NotApplicableStringConstant));

			var members = await _unitOfWork.Repository<MemberDetails>().GetAllAsync(filter: m => m.IsActive, includeProperties: nameof(MemberDetails.MembershipStatusMapping));
			return members;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodEndedMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, FitGymToolConstants.NotApplicableStringConstant));
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
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, memberEmail));

			var member = await _unitOfWork.Repository<MemberDetails>().GetAsync(
				filter: m => m.MemberEmail == memberEmail && m.IsActive, tracked: true, includeProperties: nameof(MemberDetails.MembershipStatusMapping));
			return member;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodEndedMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, memberEmail));
		}
	}

	/// <summary>
	/// Updates an existing member's details asynchronously.
	/// </summary>
	/// <param name="memberDetails">The updated member details.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> UpdateMemberAsync(MemberDetails memberDetails)
	{
		try
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(UpdateMemberAsync), DateTime.UtcNow, memberDetails.MemberEmail));

			var existingMember = await _unitOfWork.Repository<MemberDetails>().FirstOrDefaultAsync(predicate: m => m.MemberId == memberDetails.MemberId && m.IsActive);
			if (existingMember is null)
			{
				var ex = new InvalidOperationException(ExceptionConstants.ValidationErrorMessages.MemberNotFoundMessageConstant);
				_logger.LogError(ex, string.Format(
					CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMemberAsync), DateTime.UtcNow, ex.Message));
				throw ex;
			}

			// Update the entity
			_unitOfWork.Repository<MemberDetails>().Update(memberDetails);
			await _unitOfWork.SaveChangesAsync();

			return true;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMemberAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodEndedMessageConstant, nameof(UpdateMemberAsync), DateTime.UtcNow, memberDetails.MemberEmail));
		}
	}

	/// <summary>
	/// Deletes a member by MemberId asynchronously.
	/// </summary>
	/// <param name="memberId">The member's ID.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> DeleteMemberAsync(int memberId)
	{
		try
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(DeleteMemberAsync), DateTime.UtcNow, memberId));

			var member = await _unitOfWork.Repository<MemberDetails>().FirstOrDefaultAsync(predicate: m => m.MemberId == memberId && m.IsActive);
			if (member is null)
			{
				var ex = new InvalidOperationException(ExceptionConstants.ValidationErrorMessages.MemberNotFoundMessageConstant);
				_logger.LogError(ex, string.Format(
					CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(DeleteMemberAsync), DateTime.UtcNow, ex.Message));
				throw ex;
			}

			member.IsActive = false;
			_unitOfWork.Repository<MemberDetails>().Update(member);
			await _unitOfWork.SaveChangesAsync();

			return true;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(DeleteMemberAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodEndedMessageConstant, nameof(DeleteMemberAsync), DateTime.UtcNow, memberId));
		}
	}
}
