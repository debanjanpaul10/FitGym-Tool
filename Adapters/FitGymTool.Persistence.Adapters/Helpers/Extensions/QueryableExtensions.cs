// *********************************************************************************
//	<copyright file="QueryableExtensions.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>Class containing extension methods for IQueryable to filter entities.</summary>
// *********************************************************************************

using System.Reflection;
using System.Linq.Expressions;

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
}
