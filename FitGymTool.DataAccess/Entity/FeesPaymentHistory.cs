// *********************************************************************************
//	<copyright file="FeesPaymentHistory.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fees Payment History Entity Model.</summary>
// *********************************************************************************


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
	/// Gets or sets the payment status.
	/// </summary>
	/// <value>
	/// The payment status.
	/// </value>
	public int PaymentStatus { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this instance is active.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
	/// </value>
	public bool IsActive { get; set; }

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
}
