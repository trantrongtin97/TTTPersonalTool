namespace TTT.Framework.EfCore;

public interface IReadOnlyBasicRepository<TEntity>
     where TEntity : class, IEntity
{
    /// <summary>
    /// Get a list TEntity without tracking
    /// Usefull for get and edit list data
    /// </summary>
    /// <returns>List of TEntity</returns>
    Task<List<TEntity>> GetListAsync();
    /// <summary>
    /// Get a list TEntity has tracking - lighter than getlist
    /// Usefull for only view data
    /// </summary>
    /// <returns>List of TEntity no tracking</returns>
    Task<List<TEntity>> GetListNoTrackingAsync();
    /// <summary>
    /// Get a list TEntity with pagging depend on para input
    /// </summary>
    /// <param name="skipCount">number record to skip in previous page</param>
    /// <param name="maxResultCount">max number record get to page</param>
    /// <param name="sorting">property to sorting</param>
    /// <returns></returns>
    Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting);
}
