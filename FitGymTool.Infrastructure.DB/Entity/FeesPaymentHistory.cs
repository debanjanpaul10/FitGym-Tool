// *********************************************************************************
//	<copyright file="FeesPaymentHistory.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fees Payment History Entity Model.</summary>
// *********************************************************************************

using FitGymTool.Infrastructure.DB.Entity.Mapping;

namespace FitGymTool.Infrastructure.DB.Entity;

/// <summary>
/// The Fees Payment History Entity Model.
/// </summary>
/// <seealso cref="BaseEntity"/>
public class FeesPaymentHistory : BaseEntity
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
