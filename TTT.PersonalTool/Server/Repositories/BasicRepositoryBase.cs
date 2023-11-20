using System.Diagnostics.CodeAnalysis;
using TTT.PersonalTool.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using TTT.PersonalTool.Shared.Models.Interfaces;
using TTT.PersonalTool.Server.DbContexts;

namespace TTT.PersonalTool.Server.Repositories;

public class BasicRepositoryBase<TEntity> : IBasicRepository<TEntity>, IDisposable
        where TEntity : class, IEntity
{
    private readonly DbPersonalToolContext _context;

    public BasicRepositoryBase(DbPersonalToolContext context)
    {
        _context = context;
    }

    DbContext IBasicRepository<TEntity>.GetContext()
    {
        return _context;
    }

    public virtual Task DeleteAsync([NotNull] TEntity entity, bool autoSave = false)
    {
        _context.Set<TEntity>().Remove(entity);
        if (autoSave)
        {
            _context.SaveChanges();
        }
        return Task.CompletedTask;
    }

    public virtual Task DeleteManyAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false)
    {
        _context.RemoveRange(entities);

        if (autoSave)
        {
            _context.SaveChanges();
        }
        return Task.CompletedTask;
    }

    public virtual async Task<List<TEntity>> GetListAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<List<TEntity>> GetListNoTrackingAsync()
    {
        return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public virtual async Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting)
    {
        var fla = string.IsNullOrWhiteSpace(sorting);
        return await _context.Set<TEntity>()
                        .OrderByIf<TEntity, IQueryable<TEntity>>(!fla, sorting)
                        .PageBy(skipCount, maxResultCount)
                        .ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync([NotNull] int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public virtual Task<TEntity> InsertAsync([NotNull] TEntity entity, bool autoSave = false)
    {
        var savedEntity = _context.Set<TEntity>().Add(entity).Entity;

        if (autoSave)
        {
            _context.SaveChanges();
        }

        return Task.FromResult(savedEntity);
    }

    public virtual Task InsertManyAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false)
    {
        var entityArray = entities.ToArray();
        _context.Set<TEntity>().AddRange(entityArray);

        if (autoSave)
        {
            _context.SaveChanges();
        }
        return Task.CompletedTask;
    }

    public virtual Task<TEntity> UpdateAsync([NotNull] TEntity entity, bool autoSave = false)
    {
        _context.Attach(entity);
        var updatedEntity = _context.Update(entity).Entity;
        if (autoSave)
        {
            _context.SaveChanges();
        }
        return Task.FromResult(updatedEntity);
    }

    public virtual Task UpdateManyAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false)
    {
        _context.Set<TEntity>().UpdateRange(entities);

        if (autoSave)
        {
            _context.SaveChanges();
        }
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

}
