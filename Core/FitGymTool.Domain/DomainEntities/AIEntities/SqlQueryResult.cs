// *********************************************************************************
//	<copyright file="SqlQueryResultDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The SQL Query Result DTO.</summary>
// *********************************************************************************

namespace FitGymTool.Domain.DomainEntities.AIEntities;

/// <summary>
/// The SQL Query Result DTO.
/// </summary>
public class SqlQueryResult
{
	/// <summary>
	/// Gets or sets the json query.
	/// </summary>
	/// <value>
	/// The json query.
	/// </value>
	public string JsonQuery { get; set; } = string.Empty;
}
