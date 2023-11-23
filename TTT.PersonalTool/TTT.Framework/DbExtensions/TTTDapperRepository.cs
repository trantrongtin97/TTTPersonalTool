using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace TTT.Framework.DbExtensions;

public class TTTDapperRepository<TDbContext> : ITTTDapperRepository
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;
    public TTTDapperRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [Obsolete("Use GetDbConnectionAsync method.")]
    public IDbConnection DbConnection => _dbContext.Database.GetDbConnection();

    [Obsolete("Use GetDbTransactionAsync method.")]
    public IDbTransaction DbTransaction => _dbContext.Database.CurrentTransaction?.GetDbTransaction();

    public async Task<int> ExcuteAsync(string sql,object? para = null, CommandType? commandType = null, int? commandTimeOut = null)
    {
        return await DbConnection.ExecuteAsync(sql, para, DbTransaction, commandTimeOut, commandType);
    }

    public async Task<IDataReader> ExecuteReaderAsync(string sql, object? para = null, CommandType? commandType = null, int? commandTimeOut = null)
    {
        return await DbConnection.ExecuteReaderAsync(sql, para, DbTransaction, commandTimeOut, commandType);
    }

    public async Task<T> ExecuteScalarAsync<T>(string sql, object? para = null, CommandType? commandType = null, int? commandTimeOut = null)
    {
        return await DbConnection.ExecuteScalarAsync<T>(sql, para, DbTransaction, commandTimeOut, commandType);
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

