// *********************************************************************************
//	<copyright file="FeesStatus.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fees Status Entity Class.</summary>
// *********************************************************************************

using FitGymTool.Infrastructure.DB.Entity.Mapping;

namespace FitGymTool.Infrastructure.DB.Entity;

/// <summary>
/// The Fees Status Entity Class.
/// </summary>
/// <seealso cref="BaseEntity"/>
public class FeesStatus : BaseEntity
{
	/// <summary>
	/// Gets or sets the Fees Status ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the Member GUID.
	/// </summary>
	public Guid MemberGuid { get; set; }

	/// <summary>
	/// Gets or sets the Member ID.
	/// </summary>
	public int MemberId { get; set; }

	/// <summary>
	/// Gets or sets the Fees Amount.
	/// </summary>
	public decimal? FeesAmountDue { get; set; }

	/// <summary>
	/// Gets or sets the Fees Due Date.
	/// </summary>
	public DateTime DueDate { get; set; }

	/// <summary>
	/// Gets or sets the Last Payment Date.
	/// </summary>
	public DateTime? LastPaymentDate { get; set; }

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
	/// Gets or sets the Payment Status ID.
	/// </summary>
	public int PaymentStatusId { get; set; }

	/// <summary>
	/// Gets or sets the fees duration identifier.
	/// </summary>
	/// <value>
	/// The fees duration identifier.
	/// </value>
	public int FeesDurationId { get; set; }

	#region NAVIGATION FIELDS

	/// <summary>
	/// Gets or sets the member.
	/// </summary>
	/// <value>
	/// The member.
	/// </value>
	public MemberDetails Member { get; set; } = new MemberDetails();

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
