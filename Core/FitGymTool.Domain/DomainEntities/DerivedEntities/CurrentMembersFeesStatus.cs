// *********************************************************************************
//	<copyright file="CurrentMembersFeesStatus.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Current Member Fees Status Domain Entity.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.DomainEntities.DerivedEntities;

/// <summary>
/// The Current Member Fees Status Domain Entity.
/// </summary>
public class CurrentMembersFeesStatus
{
	/// <summary>
	/// Gets or sets the member identifier.
	/// </summary>
	/// <value>
	/// The member identifier.
	/// </value>
	public int MemberId { get; set; }

	/// <summary>
	/// Gets or sets the name of the member.
	/// </summary>
	/// <value>
	/// The name of the member.
	/// </value>
	public string MemberName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the member email.
	/// </summary>
	/// <value>
	/// The member email.
	/// </value>
	public string MemberEmail { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the member status.
	/// </summary>
	/// <value>
	/// The member status.
	/// </value>
	public string MemberStatus { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the fees amount due.
	/// </summary>
	/// <value>
	/// The fees amount due.
	/// </value>
	public decimal FeesAmountDue { get; set; }

	/// <summary>
	/// Gets or sets the due date.
	/// </summary>
	/// <value>
	/// The due date.
	/// </value>
	public DateTime DueDate { get; set; }

	/// <summary>
	/// Gets or sets the last payment date.
	/// </summary>
	/// <value>
	/// The last payment date.
	/// </value>
	public DateTime LastPaymentDate { get; set; }

	/// <summary>
	/// Gets or sets the fees payment status.
	/// </summary>
	/// <value>
	/// The fees payment status.
	/// </value>
	public string FeesPaymentStatus { get; set; } = string.Empty;
}
