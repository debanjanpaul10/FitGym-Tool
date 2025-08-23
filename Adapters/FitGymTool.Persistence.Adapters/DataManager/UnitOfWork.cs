// *********************************************************************************
//	<copyright file="UnitOfWork.cs" company="Personal">
//		Copyright (c) 2025 <Debanjan's Lab>
//	</copyright>
// <summary>The Unit of Work Class.</summary>
// *********************************************************************************

using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using FitGymTool.Persistence.Adapters.Contracts;
using FitGymTool.Persistence.Adapters.Repositories;
using System.Diagnostics.CodeAnalysis;
using FitGymTool.Domain.DomainEntities.DerivedEntities;
using FitGymTool.Persistence.Adapters.DatabaseContext;
using System.Data.Common;
using System.Reflection;
using FitGymTool.Persistence.Adapters.Helpers.Extensions;

namespace FitGymTool.Persistence.Adapters.DataManager;

/// <summary>
/// The Unit of Work Class.
/// </summary>
/// <param name="dbContext">The sql db context.</param>
/// <seealso cref="IUnitOfWork"/>
[ExcludeFromCodeCoverage]
public class UnitOfWork(SqlDbContext dbContext) : IUnitOfWork
{
    /// <summary>
    /// The repositories dictionary to hold repositories for different entity types.
    /// </summary>
    private readonly Dictionary<Type, object> _repositories = [];

    /// <summary>
    /// The transaction for the unit of work.
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private IDbContextTransaction _transaction;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    /// <summary>
    /// This method returns a repository for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <returns>The generic entity type.</returns>
    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);
        if (!_repositories.TryGetValue(type, out var repository))
        {
            repository = new GenericRepository<TEntity>(dbContext);
            _repositories[type] = repository;
        }

        return (IRepository<TEntity>)repository;
    }

    /// <summary>
    /// This method begins a new transaction asynchronously.
    /// </summary>
    /// <returns>A task to wait on.</returns>
    public async Task BeginTransactionAsync()
    {
        _transaction = await dbContext.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// Commits all changes made in this context to the database asynchronously.
    /// </summary>
    /// <returns>A task to wait on.</returns>
    public async Task CommitAsync()
    {
        await dbContext.SaveChangesAsync();
        if (_transaction is not null)
        {
            await _transaction.CommitAsync();
        }
    }

    /// <summary>
    /// Rollbacks all changes made in this context to the database asynchronously.
    /// </summary>
    /// <returns>A task to wait on.</returns>
    public async Task RollbackAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync();
        }
    }

    /// <summary>
    /// This method saves all changes made in this context to the database asynchronously.
    /// </summary>
    /// <returns>The save changes count.</returns>
    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Disposes the unit of work, releasing all resources.
    /// </summary>
    public void Dispose()
    {
        dbContext.Dispose();
        _transaction?.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Executes the SQL query or command asynchronously. For entity types, returns mapped results. For scalar types (bool, int), returns result based on rows affected.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="sql">The SQL or stored procedure command.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>The SQL query response as a list of T.</returns>
    public async Task<List<T>> ExecuteSqlQueryAsync<T>(string sql, params object[] parameters)
    {
        if (typeof(T) == typeof(CurrentMonthFeesAndRevenueStatus))
        {
            return await dbContext.CurrentMonthFeesAndRevenueStatus.FromSqlRaw(sql, parameters).Cast<T>().ToListAsync();
        }
        if (typeof(T) == typeof(CurrentMembersFeesStatus))
        {
            return await dbContext.CurrentMemberFeesStatus.FromSqlRaw(sql, parameters).Cast<T>().ToListAsync();
        }
        if (typeof(T) == typeof(MemberPaymentHistoryData))
        {
            return await dbContext.MemberPaymentHistoryData.FromSqlRaw(sql, parameters).Cast<T>().ToListAsync();
        }

        // For scalar types, treat as non-query and return rows affected or success as bool
        if (typeof(T) == typeof(bool))
        {
            var rows = await dbContext.Database.ExecuteSqlRawAsync(sql, parameters);
            return [(T)(object)(rows > 0)];
        }
        if (typeof(T) == typeof(int))
        {
            var rows = await dbContext.Database.ExecuteSqlRawAsync(sql, parameters);
            return [(T)(object)rows];
        }

        // For SPs that do not return anything, use T=object or T=bool and return an empty list after execution.
        // typeof(void) is not valid in generics, so we cannot check for it directly.
        // Usage: await ExecuteSqlQueryAsync<object>(sql, params) or ExecuteSqlQueryAsync<bool>(sql, params)
        if (typeof(T) == typeof(object))
        {
            await dbContext.Database.ExecuteSqlRawAsync(sql, parameters);
            return [];
        }

        throw new NotSupportedException($"Raw SQL query for type {typeof(T).Name} is not supported.");
    }

    /// <summary>
    /// Executes the SQL query raw asynchronous using ADO.NET.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="sqlQuery">The SQL query.</param>
    /// <returns>The SQL response.</returns>
    public async Task<TResponse> ExecuteSqlQueryRawAsync<TResponse>(string sqlQuery)
    {
        using var connection = dbContext.Database.GetDbConnection();
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = sqlQuery;

        using var reader = await command.ExecuteReaderAsync();

        if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(List<>))
        {
            var elementType = typeof(TResponse).GetGenericArguments()[0];
            var list = (System.Collections.IList)Activator.CreateInstance<TResponse>()!;

            while (await reader.ReadAsync())
            {
                var item = reader.MapReaderToObjectOrDictionary(elementType);
                list.Add(item);
            }

            return (TResponse)list;
        }
        else
        {
            if (await reader.ReadAsync())
            {
                var result = reader.MapReaderToObjectOrDictionary(typeof(TResponse));
                return (TResponse)result;
            }

            return default!;
        }
    }
}
