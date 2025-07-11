// *********************************************************************************
//	<copyright file="IUnitOfWork.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Unit of Work Interface.</summary>
// *********************************************************************************

using FitGymTool.DataAccess.Contracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace FitGymTool.DataAccess.Services;

/// <summary>
/// The Unit of Work Class.
/// </summary>
/// <seealso cref="IUnitOfWork"/>
public class UnitOfWork : IUnitOfWork
{
	/// <summary>
	/// The SQL DB Context.
	/// </summary>
	private readonly SqlDbContext _dbContext;

	/// <summary>
	/// The repositories dictionary to hold repositories for different entity types.
	/// </summary>
	private readonly Dictionary<Type, object> _repositories;

	/// <summary>
	/// The transaction for the unit of work.
	/// </summary>
	private IDbContextTransaction _transaction;

	/// <summary>
	/// The members data service.
	/// </summary>
	public IMembersDataService MembersDataService { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="UnitOfWork"/> class.
	/// </summary>
	/// <param name="dbContext">The sql db context.</param>
	/// <param name="MembersDataService">The members data service.</param>
	public UnitOfWork(SqlDbContext dbContext, IMembersDataService MembersDataService)
	{
		this._dbContext = dbContext;
		this.MembersDataService = MembersDataService;
		this._repositories = [];
	}

	/// <summary>
	/// This method returns a repository for the specified entity type.
	/// </summary>
	/// <typeparam name="TEntity">The entity type.</typeparam>
	/// <returns>The generic entity type.</returns>
	public IRepository<TEntity> Repository<TEntity>() where TEntity : class
	{
		return (IRepository<TEntity>)this._repositories[typeof(TEntity)];
	}

	/// <summary>
	/// This method begins a new transaction asynchronously.
	/// </summary>
	/// <returns>A task to wait on.</returns>
	public async Task BeginTransactionAsync()
	{
		this._transaction = await this._dbContext.Database.BeginTransactionAsync();
	}

	/// <summary>
	/// Commits all changes made in this context to the database asynchronously.
	/// </summary>
	/// <returns>A task to wait on.</returns>
	public async Task CommitAsync()
	{
		await this._dbContext.SaveChangesAsync();
		if (this._transaction is not null)
		{
			await this._transaction.CommitAsync();
		}
	}

	/// <summary>
	/// Rollbacks all changes made in this context to the database asynchronously.
	/// </summary>
	/// <returns>A task to wait on.</returns>
	public async Task RollbackAsync()
	{
		if (this._transaction is not null)
		{
			await this._transaction.RollbackAsync();
		}
	}

	/// <summary>
	/// This method saves all changes made in this context to the database asynchronously.
	/// </summary>
	/// <returns>The save changes count.</returns>
	public async Task<int> SaveChangesAsync()
	{
		return await this._dbContext.SaveChangesAsync();
	}

	/// <summary>
	/// Disposes the unit of work, releasing all resources.
	/// </summary>
	public void Dispose()
	{
		this._dbContext.Dispose();
		this._transaction?.Dispose();
	}
}
