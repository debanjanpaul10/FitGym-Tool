using FitGymTool.DataAccess.Contracts;
using FitGymTool.DataAccess.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitGymTool.DataAccess.Repositories;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly SqlDbContext _context;

    public GenericRepository(SqlDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
        return entities;
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null, int pageSize = 0, int pageNumber = 1, bool isActiveOnly = true)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        query = query.WhereIsActive(isActiveOnly);

        if (filter is not null)
        {
            query = query.Where(filter);
        }
        if (pageSize > 0)
        {
            if (pageSize > 100)
            {
                pageSize = 100;
            }
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }
        if (includeProperties is not null)
        {
            foreach (var includeProperty in includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<(List<TEntity>, int, bool)> GetAllPagedAsync(Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>? orderByProperty = null, bool ascending = true, int pageSize = 1000, int pageNumber = 1, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        bool hasNextPage = false;
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (filter is not null)
        {
            query = query.Where(filter);
        }
        if (orderByProperty is not null)
        {
            query = ascending ? query.OrderBy(orderByProperty) : query.OrderByDescending(orderByProperty);
        }
        int totalDbRecords = await query.CountAsync();
        if (pageSize > 0)
        {
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize + 1);
        }
        if (includeProperties is not null)
        {
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
        }
        var result = await query.AsNoTracking().ToListAsync();
        if (result.Count > pageSize)
        {
            hasNextPage = true;
            result.RemoveAt(result.Count - 1);
        }
        return (result, totalDbRecords, hasNextPage);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? filter = null, bool tracked = true, string? includeProperties = null, bool isActiveOnly = true)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        query = query.WhereIsActive(isActiveOnly);
        if (!tracked)
        {
            query = query.AsNoTracking();
        }
        if (filter is not null)
        {
            query = query.Where(filter);
        }
        if (includeProperties is not null)
        {
            foreach (var includedProperty in includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includedProperty);
            }
        }
        return await query.FirstOrDefaultAsync();
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public TEntity Update(TEntity entity)
    {
        _context.Set<TEntity>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
} 