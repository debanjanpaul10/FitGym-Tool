// *********************************************************************************
//	<copyright file="MembersService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Service Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.Models.Members;
using FitGymTool.Domain.Ports.In;
using FitGymTool.Domain.Ports.Out;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Domain.UseCases;

/// <summary>
/// The Members Service Class.
/// </summary>
/// <param name="logger">The logger.</param>
/// <param name="membersDataService">The Members Data Service.</param>
/// <seealso cref="IMembersService"/>
public class MembersService(IMembersDataManager membersDataService, ILogger<MembersService> logger) : IMembersService
{
	/// <summary>
	/// The members data service.
	/// </summary>
	private readonly IMembersDataManager _membersDataService = membersDataService;

	/// <summary>
	/// The logger for the Members Service.
	/// </summary>
	private readonly ILogger<MembersService> _logger = logger;

	/// <summary>
	/// Adds a new member to the database asynchronously.
	/// </summary>
	/// <param name="memberDetails">The member details data.</param>
	/// <param name="userEmail">The user email.</param>
	/// <param name="isFromAdmin">The boolean flag to indicate admin request.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> AddNewMemberAsync(AddMemberDomain memberDetails, string userEmail, bool isFromAdmin)
	{
		var effectiveEmail = isFromAdmin ? memberDetails.MemberEmail : userEmail;
		try
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, effectiveEmail));

			// Domain-side validation for DateTime fields
			if (memberDetails.MemberDateOfBirth == DateTime.MinValue || memberDetails.MemberJoinDate == DateTime.MinValue)
			{
				throw new InvalidOperationException("Invalid date values: MemberDateOfBirth and MemberJoinDate must be valid dates.");
			}

			// Set the effective email for the member
			memberDetails.MemberEmail = effectiveEmail!;
			memberDetails.MemberGuid = Guid.NewGuid();
			memberDetails.IsActive = true;
			memberDetails.CreatedBy = effectiveEmail!;
			memberDetails.DateCreated = DateTime.UtcNow;
			memberDetails.ModifiedBy = effectiveEmail!;
			memberDetails.DateModified = DateTime.UtcNow;

			// Ensure all DateTime fields are set to valid values in the domain model
			memberDetails.EnsureValidDates();

			var result = await _membersDataService.AddNewMemberAsync(memberDetails);
			return result;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, effectiveEmail));
		}
	}

	/// <summary>
	/// Gets all members from the database asynchronously.
	/// </summary>
	/// <returns>A list of MemberDetailsDomain.</returns>
	public async Task<List<MemberDetailsDomain>> GetAllMembersAsync()
	{
		try
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));

			var members = await _membersDataService.GetAllMembersAsync();
			return members;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));
		}
	}

	/// <summary>
	/// Gets a single member's details by Member's Email ID asynchronously.
	/// </summary>
	/// <param name="memberEmail">The member's Email ID.</param>
	/// <returns>The MemberDetailsDomain object if found; otherwise, null.</returns>
	public async Task<MemberDetailsDomain> GetMemberByEmailIdAsync(string memberEmail)
	{
		try
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, memberEmail));

			var member = await _membersDataService.GetMemberByEmailIdAsync(memberEmail);
			if (member is null)
			{
				var ex = new InvalidOperationException(ValidationErrorMessages.MemberNotFoundMessageConstant);
				_logger.LogError(ex, string.Format(
					CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, ex.Message));
				throw ex;
			}

			return member;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, memberEmail));
		}
	}

	/// <summary>
	/// Updates an existing member's details asynchronously.
	/// </summary>
	/// <param name="memberDetails">The updated member details.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> UpdateMemberDetailsAsync(UpdateMemberDomain memberDetails)
	{
		try
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(UpdateMemberDetailsAsync), DateTime.UtcNow, memberDetails.MemberEmail));

			var result = await _membersDataService.UpdateMemberDetailsAsync(memberDetails);
			return result;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMemberDetailsAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(UpdateMemberDetailsAsync), DateTime.UtcNow, memberDetails.MemberEmail));
		}
	}

	/// <summary>
	/// Updates the membership status asynchronous.
	/// </summary>
	/// <param name="updateMembershipStatusDomain">The update membership status domain.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> UpdateMembershipStatusAsync(UpdateMembershipStatusDomain updateMembershipStatusDomain)
	{
		try
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(UpdateMembershipStatusAsync), DateTime.UtcNow, updateMembershipStatusDomain.MemberEmailAddress));
			var result = await this._membersDataService.UpdateMembershipStatusAsync(updateMembershipStatusDomain);
			return result;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMembershipStatusAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			_logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(UpdateMembershipStatusAsync), DateTime.UtcNow, updateMembershipStatusDomain.MemberEmailAddress));
		}
	}
}
