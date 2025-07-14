// *********************************************************************************
//	<copyright file="FeesPaymentHistory.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fees Payment History Entity Model.</summary>
// *********************************************************************************

using FitGymTool.DataAccess.Entity.Mapping;

namespace FitGymTool.DataAccess.Entity;

/// <summary>
/// The Fees Payment History Entity Model.
/// </summary>
public class FeesPaymentHistory
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the member unique identifier.
	/// </summary>
	/// <value>
	/// The member unique identifier.
	/// </value>
	public Guid MemberGuid { get; set; }

	/// <summary>
	/// Gets or sets the member identifier.
	/// </summary>
	/// <value>
	/// The member identifier.
	/// </value>
	public int MemberId { get; set; }

	/// <summary>
	/// Gets or sets the amount.
	/// </summary>
	/// <value>
	/// The amount.
	/// </value>
	public decimal Amount { get; set; }

	/// <summary>
	/// Gets or sets the payment status ID.
	/// </summary>
	/// <value>
	/// The payment status.
	/// </value>
	public int PaymentStatusId { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this instance is active.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
	/// </value>
	public bool IsActive { get; set; }

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
	/// Gets or sets the date created.
	/// </summary>
	/// <value>
	/// The date created.
	/// </value>
	public DateTime DateCreated { get; set; }

	/// <summary>
	/// Gets or sets the created by.
	/// </summary>
	/// <value>
	/// The created by.
	/// </value>
	public string CreatedBy { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the date modified.
	/// </summary>
	/// <value>
	/// The date modified.
	/// </value>
	public DateTime DateModified { get; set; }

	/// <summary>
	/// Gets or sets the modified by.
	/// </summary>
	/// <value>
	/// The modified by.
	/// </value>
	public string ModifiedBy { get; set; } = string.Empty;

	#region Navigation Fields

	/// <summary>
	/// Gets or sets the member details.
	/// </summary>
	/// <value>
	/// The member details.
	/// </value>
	public MemberDetails MemberDetails { get; set; } = new MemberDetails();

	/// <summary>
	/// Gets or sets the fees payment status mapping.
	/// </summary>
	/// <value>
	/// The fees payment status mapping.
	/// </value>
	public FeesPaymentStatusMapping FeesPaymentStatusMapping { get; set; } = new FeesPaymentStatusMapping();

	/// <summary>
	/// Gets or sets the fees duration mapping.
	/// </summary>
	/// <value>
	/// The fees duration mapping.
	/// </value>
	public FeesDurationMapping FeesDurationMapping { get; set; } = new FeesDurationMapping();

	#endregion
}
