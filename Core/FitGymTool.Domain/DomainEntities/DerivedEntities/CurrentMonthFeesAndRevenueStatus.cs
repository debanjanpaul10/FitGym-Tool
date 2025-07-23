using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGymTool.Domain.DomainEntities.DerivedEntities;

/// <summary>
/// Current Month Fees and Revenue Status Entity.
/// </summary>
public class CurrentMonthFeesAndRevenueStatus
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
