// *********************************************************************************
//	<copyright file="ApiAdaptersTestsHelper.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The API Adapters Tests Helper Class.</summary>
// *********************************************************************************

using FitGymTool.API.Adapters.Models.Request;
using FitGymTool.API.Adapters.Models.Response;
using FitGymTool.Domain.Models.Members;

namespace FitGymTool.API.Adapters.UnitTests;

/// <summary>
/// The API Adapters Tests Helper Class.
/// </summary>
public static class ApiAdaptersTestsHelper
{
	/// <summary>
	/// The current logged in user
	/// </summary>
	public static readonly string CurrentLoggedInUser = "member@email.com";

	/// <summary>
	/// The random birth year counter
	/// </summary>
	public static readonly int RandomBirthYearCounter = new Random().Next(1, 20);

	/// <summary>
	/// Prepares the add member data dto.
	/// </summary>
	/// <returns>Add member DTO.</returns>
	public static AddMemberDTO PrepareAddMemberDataDto()
	{
		return new AddMemberDTO()
		{ 
			DateModifed = DateTime.Now,
			MemberAddress = "Lorem Ipsum Hello World",
			MemberDateOfBirth = DateTime.Now.AddYears(-RandomBirthYearCounter),
			MemberEmail = CurrentLoggedInUser,
			MemberGender = "Male",
			MemberJoinDate = DateTime.Now,
			MemberName = "New Member",
			MemberPhoneNumber = "123456789",
			MembershipStatus = "Active",
			ModifiedBy = CurrentLoggedInUser
		};
	}

	/// <summary>
	/// Prepares the update member data dto.
	/// </summary>
	/// <returns>Update member DTO.</returns>
	public static UpdateMemberDTO PrepareUpdateMemberDataDto()
	{
		return new UpdateMemberDTO()
		{
			MemberId = 1,
			MemberName = "Updated Member Name",
			MemberPhoneNumber = "987654321",
			MemberAddress = "Updated Address",
			MemberDateOfBirth = DateTime.Now.AddYears(-25),
			MemberJoinDate = DateTime.Now.AddMonths(-6),
			MemberGender = "Female",
			DateModifed = DateTime.Now,
			ModifiedBy = CurrentLoggedInUser
		};
	}

	/// <summary>
	/// Prepares the update membership status dto.
	/// </summary>
	/// <returns>Update membership status DTO.</returns>
	public static UpdateMembershipStatusDTO PrepareUpdateMembershipStatusDto()
	{
		return new UpdateMembershipStatusDTO()
		{
			MemberId = 1,
			MembershipStatusId = 2,
			MemberEmailAddress = CurrentLoggedInUser,
			DateModifed = DateTime.Now,
			ModifiedBy = CurrentLoggedInUser
		};
	}

	/// <summary>
	/// Prepares the member details dto.
	/// </summary>
	/// <returns>Member details DTO.</returns>
	public static MemberDetailsDTO PrepareMemberDetailsDto()
	{
		return new MemberDetailsDTO()
		{
			MemberId = 1,
			MemberGuid = Guid.NewGuid(),
			MemberName = "Test Member",
			MemberEmail = CurrentLoggedInUser,
			MemberPhoneNumber = "123456789",
			MemberAddress = "Test Address",
			MemberDateOfBirth = DateTime.Now.AddYears(-RandomBirthYearCounter),
			MemberGender = "Male",
			MemberJoinDate = DateTime.Now.AddMonths(-3),
			MembershipStatus = "Active"
		};
	}

	/// <summary>
	/// Prepares the member details domain.
	/// </summary>
	/// <returns>Member details domain.</returns>
	public static MemberDetailsDomain PrepareMemberDetailsDomain()
	{
		return new MemberDetailsDomain()
		{
			MemberId = 1,
			MemberGuid = Guid.NewGuid(),
			MemberName = "Test Member",
			MemberEmail = CurrentLoggedInUser,
			MemberPhoneNumber = "123456789",
			MemberAddress = "Test Address",
			MemberDateOfBirth = DateTime.Now.AddYears(-RandomBirthYearCounter),
			MemberGender = "Male",
			MemberJoinDate = DateTime.Now.AddMonths(-3),
			MembershipStatus = "Active"
		};
	}

	/// <summary>
	/// Prepares the list of member details domain.
	/// </summary>
	/// <returns>List of member details domain.</returns>
	public static List<MemberDetailsDomain> PrepareMemberDetailsDomainList()
	{
		return new List<MemberDetailsDomain>()
		{
			new MemberDetailsDomain()
			{
				MemberId = 1,
				MemberGuid = Guid.NewGuid(),
				MemberName = "Test Member 1",
				MemberEmail = "member1@email.com",
				MemberPhoneNumber = "123456789",
				MemberAddress = "Test Address 1",
				MemberDateOfBirth = DateTime.Now.AddYears(-25),
				MemberGender = "Male",
				MemberJoinDate = DateTime.Now.AddMonths(-6),
				MembershipStatus = "Active"
			},
			new MemberDetailsDomain()
			{
				MemberId = 2,
				MemberGuid = Guid.NewGuid(),
				MemberName = "Test Member 2",
				MemberEmail = "member2@email.com",
				MemberPhoneNumber = "987654321",
				MemberAddress = "Test Address 2",
				MemberDateOfBirth = DateTime.Now.AddYears(-30),
				MemberGender = "Female",
				MemberJoinDate = DateTime.Now.AddMonths(-3),
				MembershipStatus = "Inactive"
			}
		};
	}
}
