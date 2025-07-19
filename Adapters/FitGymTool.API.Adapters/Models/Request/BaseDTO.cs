// *********************************************************************************
//	<copyright file="BaseDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Base DTO class.</summary>
// *********************************************************************************

namespace FitGymTool.API.Adapters.Models.Request;

/// <summary>
/// The Base DTO Class.
/// </summary>
public class BaseDTO
{
	/// <summary>
	/// Gets or sets the modified by.
	/// </summary>
	/// <value>
	/// The modified by.
	/// </value>
	public string ModifiedBy { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the date modifed.
	/// </summary>
	/// <value>
	/// The date modifed.
	/// </value>
	public DateTime DateModifed { get; set; } = DateTime.UtcNow;
}
