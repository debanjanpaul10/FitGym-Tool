// *********************************************************************************
//	<copyright file="MembersDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Data Service Class.</summary>
// *********************************************************************************

using AutoMapper;
using FitGymTool.Domain.DrivenPorts;
using FitGymTool.Domain.Models.Members;
using FitGymTool.Infrastructure.DB.Contracts;
using FitGymTool.Infrastructure.DB.Entity;
using FitGymTool.Infrastructure.DB.Entity.Mapping;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static FitGymTool.Domain.Helpers.DomainConstants;

namespace FitGymTool.Infrastructure.DB.DataManager;

/// <summary>
/// The Members Data Service Class.
/// </summary>
/// <param name="unitOfWork">The unit of work.</param>
/// <param name="logger">The logger.</param>
/// <param name="mapper">The mapper.</param>
/// <seealso cref="IMembersDataManager"/>
public class MembersDataManager(IUnitOfWork unitOfWork, IMapper mapper, ILogger<MembersDataManager> logger) : IMembersDataManager
{
	/// <summary>
	/// The unit of work for database operations.
	/// </summary>
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	/// <summary>
	/// The logger for logging operations.
	/// </summary>
	private readonly ILogger<MembersDataManager> _logger = logger;

	/// <summary>
	/// The mapper
	/// </summary>
	private readonly IMapper _mapper = mapper;

	/// <summary>
	/// Adds a new member to the database asynchronously.
	/// </summary>
	/// <param name="memberDetails">The member details data.</param>
	/// <returns>The boolean result for success/failure.</returns>
	public async Task<bool> AddNewMemberAsync(AddMemberDomain memberDetails)
	{
		try
		{
			// Ensure all DateTime fields are valid before mapping
			memberDetails.EnsureValidDates();

			// Log all DateTime fields for debugging
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, memberDetails.MemberEmail));

			var existingMember = (await this._unitOfWork.Repository<MemberDetails>()
			    .FindAsync(predicate: member => member.MemberEmail == memberDetails.MemberEmail && member.IsActive)).Any();
			if (existingMember)
			{
				var ex = new InvalidOperationException(ValidationErrorMessages.MemberAlreadyExistsMessageConstant);
				this._logger.LogError(ex, string.Format(
					CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, ex.Message));
				throw ex;
			}

			// Lookup MembershipStatusMapping by status name
			var statusEntity = (await this._unitOfWork.Repository<MembershipStatusMapping>()
				.FindAsync(ms => ms.StatusName == memberDetails.MembershipStatus && ms.IsActive)).FirstOrDefault();

			var memberDetailsData = this._mapper.Map<MemberDetails>(memberDetails);
			memberDetailsData.MembershipStatusId = statusEntity?.Id ?? 0;

			await this._unitOfWork.Repository<MemberDetails>().AddAsync(memberDetailsData);
			await this._unitOfWork.SaveChangesAsync();

			return true;
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
				CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, memberDetails.MemberEmail));
		}
	}

	/// <summary>
	/// Gets all members from the database asynchronously.
	/// </summary>
	/// <returns>A list of MemberDetails.</returns>
	public async Task<List<MemberDetailsDomain>> GetAllMembersAsync()
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, HeaderConstants.NotApplicableStringConstant));

			var members = await this._unitOfWork.Repository<MemberDetails>().GetAllAsync(filter: m => m.IsActive, includeProperties: nameof(MemberDetails.MembershipStatusMapping));

			var membersDomainData = this._mapper.Map<List<MemberDetailsDomain>>(members);
			return membersDomainData;
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
	/// <returns>The MemberDetails object if found; otherwise, null.</returns>
	public async Task<MemberDetailsDomain?> GetMemberByEmailIdAsync(string memberEmail)
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetMemberByEmailIdAsync), DateTime.UtcNow, memberEmail));

			var member = await this._unitOfWork.Repository<MemberDetails>().GetAsync(
				filter: m => m.MemberEmail == memberEmail && m.IsActive, tracked: true, includeProperties: nameof(MemberDetails.MembershipStatusMapping));

			var memberDetailsDomainData = this._mapper.Map<MemberDetailsDomain>(member);
			return memberDetailsDomainData;
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
	public async Task<bool> UpdateMemberAsync(UpdateMemberDomain memberDetails)
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(UpdateMemberAsync), DateTime.UtcNow, memberDetails.MemberEmail));

			var existingMember = await this._unitOfWork.Repository<MemberDetails>().FirstOrDefaultAsync(predicate: m => m.MemberId == memberDetails.MemberId && m.IsActive);
			if (existingMember is null)
			{
				var ex = new InvalidOperationException(ValidationErrorMessages.MemberNotFoundMessageConstant);
				this._logger.LogError(ex, string.Format(
					CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMemberAsync), DateTime.UtcNow, ex.Message));
				throw ex;
			}

			// Update the entity
			var memberDetailsData = this._mapper.Map<MemberDetails>(memberDetails);
			this._unitOfWork.Repository<MemberDetails>().Update(memberDetailsData);
			await this._unitOfWork.SaveChangesAsync();

			return true;
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

			var member = await this._unitOfWork.Repository<MemberDetails>().FirstOrDefaultAsync(predicate: m => m.MemberId == memberId && m.IsActive);
			if (member is null)
			{
				var ex = new InvalidOperationException(ValidationErrorMessages.MemberNotFoundMessageConstant);
				this._logger.LogError(ex, string.Format(
					CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(DeleteMemberAsync), DateTime.UtcNow, ex.Message));
				throw ex;
			}

			member.IsActive = false;
			this._unitOfWork.Repository<MemberDetails>().Update(member);
			await this._unitOfWork.SaveChangesAsync();

			return true;
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
