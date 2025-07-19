// *********************************************************************************
//	<copyright file="DomainUtilities.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Domain Utilities class.</summary>
// *********************************************************************************

using FitGymTool.Domain.Models;
using FitGymTool.Domain.Models.Members;

namespace FitGymTool.Domain.Helpers;

/// <summary>
/// The Domain Utilities Class.
/// </summary>
public static class DomainUtilities
{
	/// <summary>
	/// Ensures all DateTime fields are set to valid values.
	/// </summary>
	public static void EnsureValidDates(this AddMemberDomain memberDetails)
	{
		if (memberDetails.MemberDateOfBirth == DateTime.MinValue)
		{
			memberDetails.MemberDateOfBirth = DateTime.UtcNow;
		}
		if (memberDetails.MemberJoinDate == DateTime.MinValue)
		{
			memberDetails.MemberJoinDate = DateTime.UtcNow;
		}
		if (memberDetails.DateCreated == DateTime.MinValue)
		{
			memberDetails.DateCreated = DateTime.UtcNow;
		}
		if (memberDetails.DateModified == DateTime.MinValue)
		{
			memberDetails.DateModified = DateTime.UtcNow;
		}
	}

	/// <summary>
	/// Prepares the bug report data domain.
	/// </summary>
	/// <param name="bugReportDataDomain">The bug report data domain.</param>
	public static void PrepareBugReportDataDomain(this BugReportDataDomain bugReportDataDomain)
	{
		bugReportDataDomain.IsActive = true;
		bugReportDataDomain.DateCreated = DateTime.UtcNow;
		bugReportDataDomain.DateModified = DateTime.UtcNow;
		bugReportDataDomain.ModifiedBy = bugReportDataDomain.CreatedBy;
	}

	/// <summary>
	/// Prepares the new member details data.
	/// </summary>
	/// <param name="memberDetails">The member details.</param>
	/// <param name="effectiveEmail">The effective email.</param>
	public static void PrepareNewMemberDetailsData(this AddMemberDomain memberDetails, string effectiveEmail)
	{
		memberDetails.MemberEmail = effectiveEmail!;
		memberDetails.MemberGuid = Guid.NewGuid();
		memberDetails.IsActive = true;
		memberDetails.CreatedBy = effectiveEmail!;
		memberDetails.DateCreated = DateTime.UtcNow;

		// Ensure all DateTime fields are set to valid values in the domain model
		memberDetails.EnsureValidDates();
	}
}
