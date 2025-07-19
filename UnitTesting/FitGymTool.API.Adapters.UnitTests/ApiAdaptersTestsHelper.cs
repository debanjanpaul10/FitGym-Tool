// *********************************************************************************
//	<copyright file="ApiAdaptersTestsHelper.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The API Adapters Tests Helper Class.</summary>
// *********************************************************************************

using FitGymTool.API.Adapters.Models.Request;

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
}
