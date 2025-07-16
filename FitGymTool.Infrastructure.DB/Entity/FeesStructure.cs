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
/// <seealso cref="BaseEntity"/>
public class FeesStructure : BaseEntity
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
