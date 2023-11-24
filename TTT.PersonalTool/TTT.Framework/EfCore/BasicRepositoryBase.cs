using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TTT.Framework.DbExtensions;

namespace TTT.Framework.EfCore;

public class BasicRepositoryBase<TEntity> : IBasicRepository<TEntity>, IDisposable
        where TEntity : class, IEntity
{
    private readonly DbContext _context;

    [Obsolete("Use GetDbConnection method.")]
    public IDbConnection DbConnection => _context.Database.GetDbConnection();

    [Obsolete("Use GetDbTransaction method.")]
    public IDbTransaction DbTransaction => _context.Database.CurrentTransaction.GetDbTransaction();

    public BasicRepositoryBase(DbContext context)
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

    public async Task<int> ExcuteAsync(string sql, object? para = null, CommandType? commandType = null, int? commandTimeOut = null)
    {
        return await DbConnection.ExecuteAsync(sql, para, DbTransaction, commandTimeOut, commandType);
    }

    public async Task<T> ExecuteScalarAsync<T>(string sql, object? para = null, CommandType? commandType = null, int? commandTimeOut = null)
    {
        return await DbConnection.ExecuteScalarAsync<T>(sql, para, DbTransaction, commandTimeOut, commandType);
    }

    public async Task<IDataReader> ExecuteReaderAsync(string sql, object? para = null, CommandType? commandType = null, int? commandTimeOut = null)
    {
        return await DbConnection.ExecuteReaderAsync(sql, para, DbTransaction, commandTimeOut, commandType);
    }

    public async Task<T> QueryFirtOrDefault<T>(string sql, object? para = null, CommandType? commandType = null, int? commandTimeOut = null)
    {
        return await DbConnection.QueryFirstOrDefaultAsync<T>(sql, para, DbTransaction, commandTimeOut, commandType);
    }

    public async Task<T> QuerySingleOrDefault<T>(string sql, object? para = null, CommandType? commandType = null, int? commandTimeOut = null)
    {
        return await DbConnection.QuerySingleOrDefaultAsync<T>(sql, para, DbTransaction, commandTimeOut, commandType);
    }
}
