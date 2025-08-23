// *********************************************************************************
//	<copyright file="QueryableExtensions.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>Class containing extension methods for IQueryable to filter entities.</summary>
// *********************************************************************************

using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace FitGymTool.Persistence.Adapters.Helpers.Extensions;

/// <summary>
/// Class containing extension methods for IQueryable to filter entities.
/// </summary>
public static class QueryableExtensions
{
	/// <summary>
	/// Filters the IQueryable to include only entities that are marked as active.
	/// </summary>
	/// <typeparam name="T">The type param</typeparam>
	/// <param name="query">The passed query.</param>
	/// <param name="isActiveOnly">The is active only boolean flag.</param>
	/// <returns>The queryable type data.</returns>
	public static IQueryable<T> WhereIsActive<T>(this IQueryable<T> query, bool isActiveOnly = true)
	{
		if (!isActiveOnly)
		{
			return query;
		}

		var property = typeof(T).GetProperty("IsActive", BindingFlags.Public | BindingFlags.Instance);
		if (property is null || property.PropertyType != typeof(bool))
		{
			return query;
		}

		var parameter = Expression.Parameter(typeof(T), "x");
		var propertyAccess = Expression.Property(parameter, property);
		var isActiveExpression = Expression.Equal(propertyAccess, Expression.Constant(true));
		var lambda = Expression.Lambda<Func<T, bool>>(isActiveExpression, parameter);

		return query.Where(lambda);
	}

	/// <summary>
	/// Maps reader to object or dictionary for dynamic types like Object.
	/// </summary>
	/// <param name="reader">The data reader.</param>
	/// <param name="type">The target type.</param>
	/// <returns>The mapped object or dictionary.</returns>
	public static object MapReaderToObjectOrDictionary(this DbDataReader reader, Type type)
	{
		// For Object type, return a dictionary with column names and values
		if (type == typeof(object))
		{
			var dictionary = new Dictionary<string, object?>();
			for (int i = 0; i < reader.FieldCount; i++)
			{
				var columnName = reader.GetName(i);
				var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
				dictionary[columnName] = value;
			}
			return dictionary;
		}

		// For specific types, use the existing extension method
		return reader.MapReaderToObject(type);
	}

	/// <summary>
	/// Maps a data reader row to an object of the specified type.
	/// </summary>
	/// <param name="reader">The data reader.</param>
	/// <param name="type">The target type.</param>
	/// <returns>The mapped object.</returns>
	private static object MapReaderToObject(this DbDataReader reader, Type type)
	{
		var instance = Activator.CreateInstance(type)!;
		var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

		for (int i = 0; i < reader.FieldCount; i++)
		{
			var columnName = reader.GetName(i);
			var property = properties.FirstOrDefault(p =>
				string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase));

			if (property != null && property.CanWrite && !reader.IsDBNull(i))
			{
				var value = reader.GetValue(i);
				if (value != null && value != DBNull.Value)
				{
					// Handle type conversion if needed
					if (property.PropertyType != value.GetType())
					{
						if (property.PropertyType.IsGenericType &&
							property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
						{
							var underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
							value = Convert.ChangeType(value, underlyingType!);
						}
						else
						{
							value = Convert.ChangeType(value, property.PropertyType);
						}
					}
					property.SetValue(instance, value);
				}
			}
		}

		return instance;
	}
}
