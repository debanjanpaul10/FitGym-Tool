// *********************************************************************************
//	<copyright file="MembersService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Service Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.Domain.DrivenPorts;
using FitGymTool.Domain.DrivingPorts;
using FitGymTool.Domain.Models.Members;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Domain.Services;

/// <summary>
/// The Members Service Class.
/// </summary>
/// <param name="logger">The logger.</param>
/// <param name="mapper">The auto mapper.</param>
/// <param name="membersDataService">The Members Data Service.</param>
/// <seealso cref="IMembersService"/>
public class MembersService(IMembersDataManager membersDataService, IMapper mapper, ILogger<MembersService> logger) : IMembersService
{
	/// <summary>
	/// The members data service.
	/// </summary>
	private readonly IMembersDataManager _membersDataService = membersDataService;

	/// <summary>
	/// The auto mapper.
	/// </summary>
	private readonly IMapper _mapper = mapper;

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
	public async Task<bool> AddNewMemberAsync(MemberDetailsDomain memberDetails, string userEmail, bool isFromAdmin)
	{
		var effectiveEmail = isFromAdmin ? memberDetails.MemberEmail : userEmail;
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, effectiveEmail));

			// Set the effective email for the member
			memberDetails.MemberEmail = effectiveEmail!;
			memberDetails.MemberGuid = Guid.NewGuid();

			var addNewMemberData = this._mapper.Map<AddMemberDomain>(memberDetails);
			addNewMemberData.IsActive = true;
			addNewMemberData.CreatedBy = effectiveEmail!;
			addNewMemberData.DateCreated = DateTime.UtcNow;
			addNewMemberData.ModifiedBy = effectiveEmail!;
			addNewMemberData.DateModified = DateTime.UtcNow;

			var result = await this._membersDataService.AddNewMemberAsync(addNewMemberData);
			return result;
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
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
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));

			var members = await this._membersDataService.GetAllMembersAsync();
			var memberDomains = this._mapper.Map<List<MemberDetailsDomain>>(members);
			return memberDomains;
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
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
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, memberEmail));

			var member = await this._membersDataService.GetMemberByEmailIdAsync(memberEmail);
			if (member is null)
			{
				var ex = new InvalidOperationException(ValidationErrorMessages.MemberNotFoundMessageConstant);
				this._logger.LogError(ex, string.Format(
					CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, ex.Message));
				throw ex;
			}
			var memberDomain = this._mapper.Map<MemberDetailsDomain>(member);
			return memberDomain;
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, memberEmail));
		}
	}

	/// <summary>
	/// Updates an existing member's details asynchronously.
	/// </summary>
	/// <param name="memberDetails">The updated member details.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> UpdateMemberAsync(MemberDetailsDomain memberDetails)
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(UpdateMemberAsync), DateTime.UtcNow, memberDetails.MemberEmail));

			var updateEntity = this._mapper.Map<UpdateMemberDomain>(memberDetails);
			var result = await this._membersDataService.UpdateMemberAsync(updateEntity);
			return result;
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMemberAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(UpdateMemberAsync), DateTime.UtcNow, memberDetails.MemberEmail));
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
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(DeleteMemberAsync), DateTime.UtcNow, memberId));

			var result = await this._membersDataService.DeleteMemberAsync(memberId);
			return result;
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(DeleteMemberAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(DeleteMemberAsync), DateTime.UtcNow, memberId));
		}
	}
}
