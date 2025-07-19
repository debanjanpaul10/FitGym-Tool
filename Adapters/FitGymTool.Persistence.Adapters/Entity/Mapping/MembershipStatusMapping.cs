// *********************************************************************************
// <copyright file="MembershipStatusMapping.cs" company="Personal">
//     Copyright (c) 2025 <Debanjan's Lab>
// </copyright>
// <summary>The Membership Status Mapping Entity Class.</summary>
// *********************************************************************************

namespace FitGymTool.Persistence.Adapters.Entity.Mapping;

/// <summary>
/// The Membership Status Mapping Entity Class.
/// </summary>
/// <seealso cref="BaseEntity"/>
public class MembershipStatusMapping : BaseEntity
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the Status Name.
	/// </summary>
	public string StatusName { get; set; } = string.Empty;
} 