// *********************************************************************************
//	<copyright file="FeesStructure.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fees Structure Entity Class.</summary>
// *********************************************************************************

using FitGymTool.Infrastructure.DB.Entity.Mapping;

namespace FitGymTool.Infrastructure.DB.Entity;

/// <summary>
/// The Fees Structure Entity Class.
/// </summary>
public class FeesStructure
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the fees duration identifier.
	/// </summary>
	/// <value>
	/// The fees duration identifier.
	/// </value>
	public int FeesDurationId { get; set; }

	/// <summary>
	/// Gets or sets the fees amount.
	/// </summary>
	/// <value>
	/// The fees amount.
	/// </value>
	public decimal FeesAmount { get; set; }

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

	#region Navigation Fields

	/// <summary>
	/// Gets or sets the fees duration mapping.
	/// </summary>
	/// <value>
	/// The fees duration mapping.
	/// </value>
	public FeesDurationMapping FeesDurationMapping { get; set; } = new FeesDurationMapping();

	#endregion
}
