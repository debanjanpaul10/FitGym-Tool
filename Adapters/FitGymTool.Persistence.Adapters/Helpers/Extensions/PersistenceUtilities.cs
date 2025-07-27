// *********************************************************************************
//	<copyright file="PersistenceUtilities.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Persistence Utilities class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;
using Microsoft.Data.SqlClient;

namespace FitGymTool.Persistence.Adapters.Helpers.Extensions;

/// <summary>
/// The Persistence Utilities class.
/// </summary>
internal static class PersistenceUtilities
{
	/// <summary>
	/// Prepares the update member data entity.
	/// </summary>
	/// <param name="existingMember">The existing member.</param>
	/// <param name="memberDetails">The member details.</param>
	internal static void PrepareUpdateMemberDataEntity(this MemberDetails existingMember, MemberDetails memberDetails)
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
	internal static void PrepareMembershipStatusUpdateDataEntity(this MemberDetails existingMember, MemberDetails updateMembershipStatusDomain)
	{
		existingMember.MembershipStatusId = updateMembershipStatusDomain.MembershipStatusId;
		existingMember.DateModified = DateTime.UtcNow;
		existingMember.ModifiedBy = updateMembershipStatusDomain.ModifiedBy;
	}

	/// <summary>
	/// Prepares the new member sp parameters.
	/// </summary>
	/// <param name="memberDetails">The member details.</param>
	/// <returns>The list of <see cref="SqlParameter"/></returns>
	internal static IEnumerable<SqlParameter> PrepareNewMemberSPParameters(NewMemberDetails memberDetails)
	{
		var parameters = new[]
		{
			new SqlParameter("@MemberEmail", memberDetails.MemberEmail ?? (object)DBNull.Value),
			new SqlParameter("@MemberName", memberDetails.MemberName ?? (object)DBNull.Value),
			new SqlParameter("@MemberPhoneNumber", memberDetails.MemberPhoneNumber ?? (object)DBNull.Value),
			new SqlParameter("@MemberAddress", memberDetails.MemberAddress ?? (object)DBNull.Value),
			new SqlParameter("@MemberGender", memberDetails.MemberGender ?? (object)DBNull.Value),
			new SqlParameter("@MemberDateOfBirth", memberDetails.MemberDateOfBirth),
			new SqlParameter("@MemberJoinDate", memberDetails.MemberJoinDate),
			new SqlParameter("@FeesDurationTypeName", memberDetails.FeesDurationTypeName),
			new SqlParameter("@CreatedBy", memberDetails.CreatedBy ?? (object)DBNull.Value)
		};

		return parameters;
	}
}
