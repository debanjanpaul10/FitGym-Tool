// *********************************************************************************
//	<copyright file="FeesStatus.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fees Status Entity Class.</summary>
// *********************************************************************************

namespace FitGymTool.DataAccess.Entity;

/// <summary>
/// The Fees Status Entity Class.
/// </summary>
public class FeesStatus
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
	public decimal FeesAmount { get; set; }

	/// <summary>
	/// Gets or sets the Fees Due Date.
	/// </summary>
	public DateTime DueDate { get; set; }

	/// <summary>
	/// Gets or sets the Last Payment Date.
	/// </summary>
	public DateTime LastPaymentDate { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this entry is active.
	/// </summary>
	public bool IsActive { get; set; }

	/// <summary>
	/// Gets or sets the Payment Status.
	/// </summary>
	public int PaymentStatus { get; set; }

}
