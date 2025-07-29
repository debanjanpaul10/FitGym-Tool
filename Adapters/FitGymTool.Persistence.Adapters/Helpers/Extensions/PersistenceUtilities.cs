// *********************************************************************************
//	<copyright file="PersistenceUtilities.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Persistence Utilities class.</summary>
// *********************************************************************************

using FitGymTool.Domain.DomainEntities;
using FitGymTool.Domain.DomainEntities.DerivedEntities;
using Microsoft.Data.SqlClient;
using System.Data;
using static FitGymTool.Persistence.Adapters.Helpers.Constants.DatabaseConstants.StoredProceduresConstants;
using static FitGymTool.Persistence.Adapters.Helpers.Constants.DatabaseConstants.TableConstants;

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
    /// <returns>The array of <see cref="SqlParameter"/></returns>
    internal static SqlParameter[] PrepareNewMemberSPParameters(NewMemberDetails memberDetails)
    {
        // Create DataTable with the structure matching AddNewMemberTableType
        var dataTable = new DataTable();
        dataTable.Columns.Add(StoredProcedures_Inputs.MemberEmail, typeof(string));
        dataTable.Columns.Add(StoredProcedures_Inputs.MemberName, typeof(string));
        dataTable.Columns.Add(StoredProcedures_Inputs.MemberPhoneNumber, typeof(string));
        dataTable.Columns.Add(StoredProcedures_Inputs.MemberAddress, typeof(string));
        dataTable.Columns.Add(StoredProcedures_Inputs.MemberGender, typeof(string));
        dataTable.Columns.Add(StoredProcedures_Inputs.MemberJoinDate, typeof(DateTime));
        dataTable.Columns.Add(StoredProcedures_Inputs.MemberDateOfBirth, typeof(DateTime));
        dataTable.Columns.Add(StoredProcedures_Inputs.FeesDurationTypeName, typeof(string));
        dataTable.Columns.Add(StoredProcedures_Inputs.CreatedBy, typeof(string));

        dataTable.Rows.Add(
            memberDetails.MemberEmail,
            memberDetails.MemberName,
            memberDetails.MemberPhoneNumber,
            memberDetails.MemberAddress,
            memberDetails.MemberGender,
            memberDetails.MemberJoinDate,
            memberDetails.MemberDateOfBirth,
            memberDetails.FeesDurationTypeName,
            memberDetails.CreatedBy
        );

        var tableParameter = new SqlParameter(StoredProcedures_Inputs.NewMemberDataInput, SqlDbType.Structured)
        {
            TypeName = AddNewMemberTableType,
            Value = dataTable
        };

        return new[] { tableParameter };
    }
}
