// *********************************************************************************
//	<copyright file="DomainTestsHelper.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Domain Tests Helper Class.</summary>
// *********************************************************************************

using FitGymTool.Domain.Models.Members;

namespace FitGymTool.Domain.UnitTests;

/// <summary>
/// The Domain Tests Helper Class.
/// </summary>
public static class DomainTestsHelper
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
    /// Prepares the add member domain data.
    /// </summary>
    /// <returns></returns>
    public static AddMemberDomain PrepareAddMemberDomainData()
    {
        return new()
        {
            DateModified = DateTime.Now,
            MemberAddress = "Lorem Ipsum Hello World",
            MemberDateOfBirth = DateTime.Now.AddYears(-RandomInteger),
            MemberEmail = CurrentLoggedInMember,
            MemberGender = "Male",
            MemberJoinDate = DateTime.Now,
            MemberName = "New Member",
            MemberPhoneNumber = "123456789",
            MembershipStatus = "Active",
            ModifiedBy = CurrentLoggedInMember
        };
    }

    /// <summary>
    /// Prepares the add member domain data with invalid dates.
    /// </summary>
    /// <returns></returns>
    public static AddMemberDomain PrepareAddMemberDomainDataWithInvalidDates()
    {
        return new()
        {
            DateModified = DateTime.Now,
            MemberAddress = "Lorem Ipsum Hello World",
            MemberDateOfBirth = DateTime.MinValue,
            MemberEmail = CurrentLoggedInMember,
            MemberGender = "Male",
            MemberJoinDate = DateTime.MinValue,
            MemberName = "New Member",
            MemberPhoneNumber = "123456789",
            MembershipStatus = "Active",
            ModifiedBy = CurrentLoggedInMember
        };
    }

    /// <summary>
    /// Prepares the update member domain data.
    /// </summary>
    /// <returns></returns>
    public static UpdateMemberDomain PrepareUpdateMemberDomainData()
    {
        return new()
        {
            MemberId = 1,
            MemberName = "Updated Member Name",
            MemberEmail = CurrentLoggedInMember,
            MemberPhoneNumber = "987654321",
            MemberAddress = "Updated Address",
            MemberDateOfBirth = DateTime.Now.AddYears(-25),
            MemberGender = "Female",
            MemberJoinDate = DateTime.Now.AddMonths(-6),
            MemberGuid = Guid.NewGuid(),
            DateModified = DateTime.Now,
            ModifiedBy = CurrentLoggedInMember
        };
    }

    /// <summary>
    /// Prepares the update membership status domain data.
    /// </summary>
    /// <returns></returns>
    public static UpdateMembershipStatusDomain PrepareUpdateMembershipStatusDomainData()
    {
        return new()
        {
            MemberId = 1,
            MembershipStatusId = 2,
            MemberEmailAddress = CurrentLoggedInMember,
            ModifiedBy = CurrentLoggedInMember
        };
    }

    /// <summary>
    /// Prepares the member details domain data.
    /// </summary>
    /// <returns></returns>
    public static MemberDetailsDomain PrepareMemberDetailsDomainData()
    {
        return new()
        {
            MemberId = 1,
            MemberGuid = Guid.NewGuid(),
            MemberName = "Test Member",
            MemberEmail = CurrentLoggedInMember,
            MemberPhoneNumber = "123456789",
            MemberAddress = "Test Address",
            MemberDateOfBirth = DateTime.Now.AddYears(-RandomInteger),
            MemberGender = "Male",
            MemberJoinDate = DateTime.Now.AddMonths(-3),
            MembershipStatus = "Active"
        };
    }

    /// <summary>
    /// Prepares the list of member details domain data.
    /// </summary>
    /// <returns></returns>
    public static List<MemberDetailsDomain> PrepareMemberDetailsDomainDataList()
    {
        return new List<MemberDetailsDomain>()
        {
            new()
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
            new()
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
