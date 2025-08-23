// *********************************************************************************
//	<copyright file="DatabaseSchemaDomain.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Database Schema Domain class.</summary>
// *********************************************************************************

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitGymTool.Domain.DomainEntities.MetadataEntities;

/// <summary>
/// The Database Schema Domain class.
/// </summary>
public class DatabaseSchemaDomain
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
	public IEnumerable<TableSchemaDomain> Value { get; set; } = [];
}

/// <summary>
/// The Table Schema Domain Class.
/// </summary>
[BsonIgnoreExtraElements]
public class TableSchemaDomain
{
	/// <summary>
	/// The Id.
	/// </summary>
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the name of the table.
	/// </summary>
	/// <value>
	/// The name of the table.
	/// </value>
	[BsonElement("tableName")]
	public string TableName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the columns.
	/// </summary>
	/// <value>
	/// The columns.
	/// </value>
	[BsonElement("columns")]
	public IEnumerable<ColumnSchemaDomain> Columns { get; set; } = [];
}

/// <summary>
/// The Column Schema Domain Class.
/// </summary>
public class ColumnSchemaDomain
{
	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <value>
	/// The name.
	/// </value>
	[BsonElement("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	/// <value>
	/// The description.
	/// </value>
	[BsonElement("description")]
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the type of the data.
	/// </summary>
	/// <value>
	/// The type of the data.
	/// </value>
	[BsonElement("dataType")]
	public string DataType { get; set; } = string.Empty;
}