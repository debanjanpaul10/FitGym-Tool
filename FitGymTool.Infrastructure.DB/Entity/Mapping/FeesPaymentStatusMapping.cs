// *********************************************************************************
//	<copyright file="FeesPaymentStatusMapping.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Fees Payment Status Mapping Entity Model.</summary>
// *********************************************************************************

namespace FitGymTool.Infrastructure.DB.Entity.Mapping;

/// <summary>
/// The Fees Payment Status Mapping Entity Model.
/// </summary>
/// <seealso cref="BaseEntity"/>
public class FeesPaymentStatusMapping : BaseEntity
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
}
