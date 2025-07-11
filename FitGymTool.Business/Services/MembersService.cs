// *********************************************************************************
//	<copyright file="MembersService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Service Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.Business.Contracts;
using FitGymTool.DataAccess.Contracts;
using FitGymTool.DataAccess.Entity;
using FitGymTool.Shared.Constants;
using FitGymTool.Shared.DTOs.Members;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace FitGymTool.Business.Services;

/// <summary>
/// The Members Service Class.
/// </summary>
/// <param name="logger">The logger.</param>
/// <param name="mapper">The auto mapper.</param>
/// <param name="membersDataService">The Members Data Service.</param>
/// <seealso cref="IMembersService"/>
public class MembersService(IMembersDataService membersDataService, IMapper mapper, ILogger<MembersService> logger) : IMembersService
{
	/// <summary>
	/// The members data service.
	/// </summary>
	private readonly IMembersDataService _membersDataService = membersDataService;

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
	/// <param name="isFromAdmin">The boolean flag to indicate admin request.</param>
	/// <param name="userEmail">The user email.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> AddNewMemberAsync(AddMemberDTO memberDetails, string userEmail, bool isFromAdmin)
	{
		var effectiveEmail = isFromAdmin ? memberDetails.MemberEmail : userEmail;
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, effectiveEmail));

			var addNewMemberData = this._mapper.Map<MemberDetails>(memberDetails);
			addNewMemberData.MemberGuid = Guid.NewGuid();
			addNewMemberData.IsActive = true;
			addNewMemberData.MemberEmail = effectiveEmail!;

			var result = await this._membersDataService.AddNewMemberAsync(addNewMemberData);
			return result;
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodEndedMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, effectiveEmail));
		}
	}

	/// <summary>
	/// Gets all members from the database asynchronously.
	/// </summary>
	/// <returns>A list of MemberDetailsDTO.</returns>
	public async Task<List<MemberDetailsDTO>> GetAllMembersAsync()
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, FitGymToolConstants.NotApplicableStringConstant));

			var members = await this._membersDataService.GetAllMembersAsync();
			var memberDTOs = this._mapper.Map<List<MemberDetailsDTO>>(members);
			return memberDTOs;
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodEndedMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, FitGymToolConstants.NotApplicableStringConstant));
		}
	}

	/// <summary>
	/// Gets a single member's details by MemberId asynchronously.
	/// </summary>
	/// <param name="memberId">The member's ID.</param>
	/// <returns>The MemberDetailsDTO object if found; otherwise, null.</returns>
	public async Task<MemberDetailsDTO> GetMemberByIdAsync(int memberId)
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(GetMemberByIdAsync), DateTime.UtcNow, memberId));

			var member = await this._membersDataService.GetMemberByIdAsync(memberId);
			if (member is null)
			{
				var ex = new InvalidOperationException(ExceptionConstants.ValidationErrorMessages.MemberNotFoundMessageConstant);
				this._logger.LogError(ex, string.Format(
					CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMemberByIdAsync), DateTime.UtcNow, ex.Message));
				throw ex;
			}
			var memberDTO = this._mapper.Map<MemberDetailsDTO>(member);
			return memberDTO;
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(GetMemberByIdAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodEndedMessageConstant, nameof(GetMemberByIdAsync), DateTime.UtcNow, memberId));
		}
	}

	/// <summary>
	/// Updates an existing member's details asynchronously.
	/// </summary>
	/// <param name="memberDetails">The updated member details.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> UpdateMemberAsync(UpdateMemberDTO memberDetails)
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(UpdateMemberAsync), DateTime.UtcNow, memberDetails.MemberId));

			var updateEntity = this._mapper.Map<MemberDetails>(memberDetails);
			var result = await this._membersDataService.UpdateMemberAsync(updateEntity);
			return result;
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMemberAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodEndedMessageConstant, nameof(UpdateMemberAsync), DateTime.UtcNow, memberDetails.MemberId));
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
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(DeleteMemberAsync), DateTime.UtcNow, memberId));

			var result = await this._membersDataService.DeleteMemberAsync(memberId);
			return result;
		}
		catch (Exception ex)
		{
			this._logger.LogError(ex, string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(DeleteMemberAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodEndedMessageConstant, nameof(DeleteMemberAsync), DateTime.UtcNow, memberId));
		}
	}
}
