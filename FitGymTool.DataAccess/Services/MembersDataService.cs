// *********************************************************************************
//	<copyright file="IMembersDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Members Data Service Interface.</summary>
// *********************************************************************************

using FitGymTool.DataAccess.Contracts;
using FitGymTool.DataAccess.Entity;
using FitGymTool.Shared.Constants;
using Microsoft.Extensions.Logging;
using System.Data.Entity;
using System.Globalization;

namespace FitGymTool.DataAccess.Services;

/// <summary>
/// The Members Data Service Class.
/// </summary>
/// <param name="dbContext">The SQL database context.</param>
/// <param name="logger">The logger.</param>
/// <seealso cref="IMembersDataService"/>
public class MembersDataService(SqlDbContext dbContext, ILogger<MembersDataService> logger) : IMembersDataService
{
	/// <summary>
	/// The SQL database context.
	/// </summary>
	private readonly SqlDbContext _dbContext = dbContext;

	/// <summary>
	/// The logger service.
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
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, memberDetails.MemberEmail));

			var existingMember = await this._dbContext.MemberDetails
				.AnyAsync(member => member.MemberId == memberDetails.MemberId && member.MemberGuid == memberDetails.MemberGuid && member.IsActive);
			if (existingMember)
			{
				var ex = new InvalidOperationException(ExceptionConstants.ValidationErrorMessages.MemberAlreadyExistsMessageConstant);
				this._logger.LogError(ex, string.Format(
					CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(AddNewMemberAsync), DateTime.UtcNow, ex.Message));
				throw ex;
			}

			await this._dbContext.MemberDetails.AddAsync(memberDetails);
			await this._dbContext.SaveChangesAsync();

			return true;
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
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, "N/A"));

			var members = await this._dbContext.MemberDetails.Where(m => m.IsActive).ToListAsync();
			return members;
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
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodEndedMessageConstant, nameof(GetAllMembersAsync), DateTime.UtcNow, "N/A"));
		}
	}

	/// <summary>
	/// Gets a single member's details by MemberId asynchronously.
	/// </summary>
	/// <param name="memberId">The member's ID.</param>
	/// <returns>The MemberDetails object if found; otherwise, null.</returns>
	public async Task<MemberDetails?> GetMemberByIdAsync(int memberId)
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(GetMemberByIdAsync), DateTime.UtcNow, memberId));

			var member = await this._dbContext.MemberDetails.FirstOrDefaultAsync(m => m.MemberId == memberId && m.IsActive);
			return member;
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
	public async Task<bool> UpdateMemberAsync(MemberDetails memberDetails)
	{
		try
		{
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(UpdateMemberAsync), DateTime.UtcNow, memberDetails.MemberEmail));

			var existingMember = await this._dbContext.MemberDetails.FirstOrDefaultAsync(m => m.MemberId == memberDetails.MemberId && m.IsActive);
			if (existingMember == null)
			{
				var ex = new InvalidOperationException(ExceptionConstants.ValidationErrorMessages.MemberNotFoundMessageConstant);
				this._logger.LogError(ex, string.Format(
					CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(UpdateMemberAsync), DateTime.UtcNow, ex.Message));
				throw ex;
			}

			this._dbContext.Entry(existingMember).CurrentValues.SetValues(memberDetails);
			await this._dbContext.SaveChangesAsync();

			return true;
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
			this._logger.LogInformation(string.Format(
				CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodStartedMessageConstant, nameof(DeleteMemberAsync), DateTime.UtcNow, memberId));

			var member = await this._dbContext.MemberDetails.FirstOrDefaultAsync(m => m.MemberId == memberId && m.IsActive);
			if (member == null)
			{
				var ex = new InvalidOperationException(ExceptionConstants.ValidationErrorMessages.MemberNotFoundMessageConstant);
				this._logger.LogError(ex, string.Format(
					CultureInfo.CurrentCulture, ExceptionConstants.LoggingConstants.MethodFailedWithMessageConstant, nameof(DeleteMemberAsync), DateTime.UtcNow, ex.Message));
				throw ex;
			}

			member.IsActive = false;
			await this._dbContext.SaveChangesAsync();

			return true;
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
