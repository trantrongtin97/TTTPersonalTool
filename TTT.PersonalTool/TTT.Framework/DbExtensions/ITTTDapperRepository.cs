using System.Data;

namespace TTT.Framework.DbExtensions;

internal interface ITTTDapperRepository
{
    [Obsolete("Use GetDbConnectionAsync method.")]
    IDbConnection DbConnection { get; }

    [Obsolete("Use GetDbTransactionAsync method.")]
    IDbTransaction DbTransaction { get; }

    Task<int> ExcuteAsync(string sql,object? para = null, CommandType? commandType = null,int? commandTimeOut = null);
    Task<T> ExecuteScalarAsync<T>(string sql, object? para = null, CommandType? commandType = null, int? commandTimeOut = null);
    Task<IDataReader> ExecuteReaderAsync(string sql, object? para = null, CommandType? commandType = null, int? commandTimeOut = null);
    Task<T> QueryFirtOrDefault<T>(string sql, object? para = null, CommandType? commandType = null, int? commandTimeOut = null);
    Task<T> QuerySingleOrDefault<T>(string sql, object? para = null, CommandType? commandType = null, int? commandTimeOut = null);
}
