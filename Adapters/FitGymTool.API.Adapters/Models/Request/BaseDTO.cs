// *********************************************************************************
//	<copyright file="BaseDTO.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
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
	public DateTime DateModified { get; set; } = DateTime.UtcNow;
}
