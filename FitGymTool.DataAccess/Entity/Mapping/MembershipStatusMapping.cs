// *********************************************************************************
// <copyright file="MembershipStatusMapping.cs" company="Personal">
//     Copyright (c) 2025 Personal
// </copyright>
// <summary>The Membership Status Mapping Entity Class.</summary>
// *********************************************************************************

namespace FitGymTool.DataAccess.Entity.Mapping;

/// <summary>
/// The Membership Status Mapping Entity Class.
/// </summary>
public class MembershipStatusMapping
{
	/// <summary>
	/// Gets or sets the Id (identity column).
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the Status Name.
	/// </summary>
	public string StatusName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the Status Id (primary key, used as FK in MemberDetails).
	/// </summary>
	public int StatusId { get; set; }

	/// <summary>
	/// Gets or sets whether the status is active.
	/// </summary>
	public bool IsActive { get; set; }

	/// <summary>
	/// Gets or sets the date created.
	/// </summary>
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