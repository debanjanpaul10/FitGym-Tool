// *********************************************************************************
//	<copyright file="PersistenceTestsHelper.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Persistence Tests Helper Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.Mapping;

namespace FitGymTool.Persistence.Adapters.UnitTests;

/// <summary>
/// The Persistence Tests Helper Class.
/// </summary>
public static class PersistenceTestsHelper
{
	/// <summary>
	/// The current logged in member
	/// </summary>
	public static readonly string CurrentLoggedInMember = "user@email.com";

	/// <summary>
	/// The random integer
	/// </summary>
	public static readonly int RandomInteger = new Random().Next(1, 20);

	/// <summary>
	/// Creates a valid AddMemberDomain for testing.
	/// </summary>
	/// <returns>A valid AddMemberDomain instance.</returns>
	public static MemberDetails CreateValidAddMemberDomain()
	{
		return new MemberDetails
		{
			MemberName = "John Doe",
			MemberEmail = CurrentLoggedInMember,
			MemberPhoneNumber = "1234567890",
			MemberAddress = "123 Test Street",
			MemberDateOfBirth = new DateTime(1990, 1, 1),
			MemberGender = "Male",
			MemberJoinDate = DateTime.UtcNow.Date,
			MembershipStatusId = 1,
			MemberGuid = Guid.NewGuid(),
			IsActive = true,
			DateCreated = DateTime.UtcNow,
			CreatedBy = "TestUser",
			DateModified = DateTime.UtcNow,
			ModifiedBy = "TestUser"
		};
	}

	/// <summary>
	/// Creates a valid UpdateMemberDomain for testing.
	/// </summary>
	/// <param name="memberId">The member ID.</param>
	/// <param name="email">The email address (optional).</param>
	/// <returns>A valid UpdateMemberDomain instance.</returns>
	public static MemberDetails CreateValidUpdateMemberDomain()
	{
		return new MemberDetails
		{
			MemberId = new Random().Next(1, 10),
			MemberName = "Jane Doe",
			MemberEmail = CurrentLoggedInMember,
			MemberPhoneNumber = "0987654321",
			MemberAddress = "456 Updated Street",
			MemberDateOfBirth = new DateTime(1985, 5, 15),
			MemberGender = "Female",
			MemberJoinDate = DateTime.UtcNow.Date.AddDays(-30),
			MemberGuid = Guid.NewGuid(),
			IsActive = true,
			DateCreated = DateTime.UtcNow.AddDays(-30),
			CreatedBy = "TestUser",
			DateModified = DateTime.UtcNow,
			ModifiedBy = "UpdatedUser"
		};
	}

	/// <summary>
	/// Creates a valid UpdateMembershipStatusDomain for testing.
	/// </summary>
	/// <param name="memberId">The member ID.</param>
	/// <param name="email">The email address (optional).</param>
	/// <returns>A valid UpdateMembershipStatusDomain instance.</returns>
	public static MemberDetails CreateValidUpdateMembershipStatusDomain(int memberId = 1)
	{
		return new MemberDetails
		{
			MemberId = memberId,
			MembershipStatusId = 2,
			MemberEmail = CurrentLoggedInMember,
			ModifiedBy = "TestUser"
		};
	}

	/// <summary>
	/// Creates a valid MemberDetails entity for testing.
	/// </summary>
	/// <param name="memberId">The member ID.</param>
	/// <param name="email">The email address (optional).</param>
	/// <returns>A valid MemberDetails entity instance.</returns>
	public static MemberDetails CreateValidMemberDetailsEntity(int memberId = 1, string email = "user@email.com")
	{
		return new MemberDetails
		{
			MemberId = memberId,
			MemberName = "John Doe",
			MemberEmail = email,
			MemberPhoneNumber = "1234567890",
			MemberAddress = "123 Test Street",
			MemberDateOfBirth = new DateTime(1990, 1, 1),
			MemberGender = "Male",
			MemberJoinDate = DateTime.UtcNow.Date,
			MembershipStatusId = 1,
			MemberGuid = Guid.NewGuid(),
			IsActive = true,
			DateCreated = DateTime.UtcNow,
			CreatedBy = "TestUser",
			DateModified = DateTime.UtcNow,
			ModifiedBy = "TestUser",
			MembershipStatusMapping = CreateValidMembershipStatusMapping()
		};
	}

	/// <summary>
	/// Creates a valid MemberDetailsDomain for testing.
	/// </summary>
	/// <param name="memberId">The member ID.</param>
	/// <param name="email">The email address (optional).</param>
	/// <returns>A valid MemberDetailsDomain instance.</returns>
	public static MemberDetails CreateValidMemberDetailsDomain(int memberId = 1, string email = "test@example.com")
	{
		return new MemberDetails
		{
			MemberId = memberId,
			MemberName = "John Doe",
			MemberEmail = email,
			MemberPhoneNumber = "1234567890",
			MemberAddress = "123 Test Street",
			MemberDateOfBirth = new DateTime(1990, 1, 1),
			MemberGender = "Male",
			MemberJoinDate = DateTime.UtcNow.Date,
			MembershipStatusId = 1,
			MemberGuid = Guid.NewGuid(),
			IsActive = true,
			DateCreated = DateTime.UtcNow,
			CreatedBy = "TestUser",
			DateModified = DateTime.UtcNow,
			ModifiedBy = "TestUser"
		};
	}

	/// <summary>
	/// Creates a valid MembershipStatusMapping entity for testing.
	/// </summary>
	/// <param name="id">The status ID.</param>
	/// <param name="statusName">The status name (optional).</param>
	/// <returns>A valid MembershipStatusMapping entity instance.</returns>
	public static MembershipStatusMapping CreateValidMembershipStatusMapping(int id = 1, string statusName = "Active")
	{
		return new MembershipStatusMapping
		{
			Id = id,
			StatusName = statusName,
			IsActive = true,
			DateCreated = DateTime.UtcNow,
			CreatedBy = "TestUser",
			DateModified = DateTime.UtcNow,
			ModifiedBy = "TestUser"
		};
	}

	/// <summary>
	/// Creates a list of valid MemberDetails entities for testing.
	/// </summary>
	/// <param name="count">The number of entities to create.</param>
	/// <returns>A list of valid MemberDetails entities.</returns>
	public static List<MemberDetails> CreateValidMemberDetailsEntityList(int count = 3)
	{
		var members = new List<MemberDetails>();
		for (int i = 1; i <= count; i++)
		{
			members.Add(CreateValidMemberDetailsEntity(i, $"test{i}@example.com"));
		}
		return members;
	}

	/// <summary>
	/// Creates a list of valid MemberDetailsDomain instances for testing.
	/// </summary>
	/// <param name="count">The number of instances to create.</param>
	/// <returns>A list of valid MemberDetailsDomain instances.</returns>
	public static List<MemberDetails> CreateValidMemberDetailsDomainList(int count = 3)
	{
		var members = new List<MemberDetails>();
		for (int i = 1; i <= count; i++)
		{
			members.Add(CreateValidMemberDetailsDomain(i, $"test{i}@example.com"));
		}
		return members;
	}
}
