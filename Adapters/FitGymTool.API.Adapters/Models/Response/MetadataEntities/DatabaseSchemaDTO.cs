// *********************************************************************************
//	<copyright file="DatabaseSchemaDTO.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Database Schema DTO class.</summary>
// *********************************************************************************

namespace FitGymTool.API.Adapters.Models.Response.MetadataEntities;

/// <summary>
/// The Database Schema DTO.
/// </summary>
public class DatabaseSchemaDTO
{
	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <value>
	/// The name.
	/// </value>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the value.
	/// </summary>
	/// <value>
	/// The value.
	/// </value>
	public IEnumerable<TableSchemaDTO> Value { get; set; } = [];
}

/// <summary>
/// The Table Schema DTO Class.
/// </summary>
public class TableSchemaDTO
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public string Id { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the name of the table.
	/// </summary>
	/// <value>
	/// The name of the table.
	/// </value>
	public string TableName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the columns.
	/// </summary>
	/// <value>
	/// The columns.
	/// </value>
	public IEnumerable<ColumnSchemaDTO> Columns { get; set; } = [];
}

/// <summary>
/// The Column Schema DTO Class.
/// </summary>
public class ColumnSchemaDTO
{
	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <value>
	/// The name.
	/// </value>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	/// <value>
	/// The description.
	/// </value>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the type of the data.
	/// </summary>
	/// <value>
	/// The type of the data.
	/// </value>
	public string DataType { get; set; } = string.Empty;
}
