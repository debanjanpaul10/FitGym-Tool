// *********************************************************************************
//	<copyright file="FeesStatus.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fees Payment Status Mapping Entity Model.</summary>
// *********************************************************************************

namespace FitGymTool.DataAccess.Entity.Mapping;

/// <summary>
/// The Fees Payment Status Mapping Entity Model.
/// </summary>
public class FeesPaymentStatusMapping
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the name of the status.
	/// </summary>
	/// <value>
	/// The name of the status.
	/// </value>
	public string StatusName { get; set; } = string.Empty;

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
	public DateTime DateCreated {  get; set; }

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
