// *********************************************************************************
//	<copyright file="MemberPaymentHistoryData.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Member Payment History Data Model.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.DomainEntities.DerivedEntities;

/// <summary>
/// The Member Payment History Data Model.
/// </summary>
public class MemberPaymentHistoryData
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
	public string MemberEmail { get; set; }  = string.Empty;

	/// <summary>
	/// Gets or sets the member status.
	/// </summary>
	/// <value>
	/// The member status.
	/// </value>
	public string MemberStatus { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the amount.
	/// </summary>
	/// <value>
	/// The amount.
	/// </value>
	public decimal Amount { get; set; }

	/// <summary>
	/// Gets or sets from date.
	/// </summary>
	/// <value>
	/// From date.
	/// </value>
	public DateTime FromDate { get; set; }

	/// <summary>
	/// Converts to date.
	/// </summary>
	/// <value>
	/// To date.
	/// </value>
	public DateTime ToDate { get; set; }

	/// <summary>
	/// Gets or sets the fees payment status.
	/// </summary>
	/// <value>
	/// The fees payment status.
	/// </value>
	public string FeesPaymentStatus { get; set; } = string.Empty;
}
