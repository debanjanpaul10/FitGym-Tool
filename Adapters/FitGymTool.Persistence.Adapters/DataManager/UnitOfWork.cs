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
	/// The SQL DB Context.
	/// </summary>
	private readonly SqlDbContext _dbContext = dbContext;
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
			repository = new GenericRepository<TEntity>(_dbContext);
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
		_transaction = await _dbContext.Database.BeginTransactionAsync();
	}

	/// <summary>
	/// Commits all changes made in this context to the database asynchronously.
	/// </summary>
	/// <returns>A task to wait on.</returns>
	public async Task CommitAsync()
	{
		await _dbContext.SaveChangesAsync();
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
		return await _dbContext.SaveChangesAsync();
	}

	/// <summary>
	/// Disposes the unit of work, releasing all resources.
	/// </summary>
	public void Dispose()
	{
		_dbContext.Dispose();
		_transaction?.Dispose();
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Executes the SQL query asynchronous.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="sql">The SQL.</param>
	/// <param name="parameters">The parameters.</param>
	/// <returns>The SQL query response.</returns>
	public async Task<List<T>> ExecuteSqlQueryAsync<T>(string sql, params object[] parameters) where T : class, new()
	{
		if (typeof(T) == typeof(CurrentMonthFeesAndRevenueStatus))
		{
			return await _dbContext.CurrentMonthFeesAndRevenueStatus.FromSqlRaw(sql).ToListAsync() as List<T> ?? [];
		}
		else if (typeof(T) == typeof(CurrentMembersFeesStatus))
		{
			return await _dbContext.CurrentMemberFeesStatus.FromSqlRaw(sql).ToListAsync() as List<T> ?? [];
		}

		throw new NotSupportedException($"Raw SQL query for type {typeof(T).Name} is not supported.");
	}
}
