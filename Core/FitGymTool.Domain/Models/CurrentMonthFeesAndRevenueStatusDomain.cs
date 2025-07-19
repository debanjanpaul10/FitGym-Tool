// *********************************************************************************
//	<copyright file="CurrentMonthFeesAndRevenueStatusDomain.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Current Month Fees and Revenue Status Data Domain.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.Models;

/// <summary>
/// The Current Month Fees and Revenue Status Data Domain.
/// </summary>
public class CurrentMonthFeesAndRevenueStatusDomain
{
	/// <summary>
	/// Gets or sets the member identifier.
	/// </summary>
	/// <value>
	/// The member identifier.
	/// </value>
	public int MemberId { get; set; }

	/// <summary>
	/// Gets or sets the member email.
	/// </summary>
	/// <value>
	/// The member email.
	/// </value>
	public string MemberEmail { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the membership status.
	/// </summary>
	/// <value>
	/// The membership status.
	/// </value>
	public string MembershipStatus { get; set; } = string.Empty;

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
	/// Gets or sets the fees status.
	/// </summary>
	/// <value>
	/// The fees status.
	/// </value>
	public string FeesStatus { get; set; } = string.Empty;
}
