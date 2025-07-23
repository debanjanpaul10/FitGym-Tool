// *********************************************************************************
//	<copyright file="PersistenceUtilities.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Persistence Utilities class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities;

namespace FitGymTool.Persistence.Adapters.Helpers.Extensions;

/// <summary>
/// The Persistence Utilities class.
/// </summary>
public static class PersistenceUtilities
{
	/// <summary>
	/// Prepares the update member data entity.
	/// </summary>
	/// <param name="existingMember">The existing member.</param>
	/// <param name="memberDetails">The member details.</param>
	public static void PrepareUpdateMemberDataEntity(this MemberDetails existingMember, MemberDetails memberDetails)
	{
		existingMember.MemberName = memberDetails.MemberName;
		existingMember.MemberPhoneNumber = memberDetails.MemberPhoneNumber;
		existingMember.MemberAddress = memberDetails.MemberAddress;
		existingMember.MemberDateOfBirth = memberDetails.MemberDateOfBirth;
		existingMember.MemberJoinDate = memberDetails.MemberJoinDate;
		existingMember.MemberGender = memberDetails.MemberGender;
		existingMember.DateModified = memberDetails.DateModified;
		existingMember.ModifiedBy = memberDetails.ModifiedBy;
	}

	/// <summary>
	/// Prepares the membership status update data entity.
	/// </summary>
	/// <param name="existingMember">The existing member.</param>
	/// <param name="updateMembershipStatusDomain">The update membership status domain.</param>
	public static void PrepareMembershipStatusUpdateDataEntity(this MemberDetails existingMember, MemberDetails updateMembershipStatusDomain)
	{
		existingMember.MembershipStatusId = updateMembershipStatusDomain.MembershipStatusId;
		existingMember.DateModified = DateTime.UtcNow;
		existingMember.ModifiedBy = updateMembershipStatusDomain.ModifiedBy;
	}
}
