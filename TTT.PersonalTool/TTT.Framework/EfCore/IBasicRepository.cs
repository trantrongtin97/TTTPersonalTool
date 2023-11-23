using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace TTT.Framework.EfCore;

public interface IBasicRepository<TEntity> : IReadOnlyBasicRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Inserts a new entity.
    /// </summary>
    /// <param name="autoSave">
    /// Set true to automatically save changes to database.
    /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
    /// </param>
    /// <param name="entity">Inserted entity</param>
    Task<TEntity> InsertAsync([NotNull] TEntity entity, bool autoSave = false);

    /// <summary>
    /// Inserts multiple new entities.
    /// </summary>
    /// <param name="autoSave">
    /// Set true to automatically save changes to database.
    /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
    /// </param>
    /// <param name="entities">Entities to be inserted.</param>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    Task InsertManyAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="autoSave">
    /// Set true to automatically save changes to database.
    /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
    /// </param>
    /// <param name="entity">Entity</param>
    Task<TEntity> UpdateAsync([NotNull] TEntity entity, bool autoSave = false);

    /// <summary>
    /// Updates multiple entities.
    /// </summary>
    /// <param name="entities">Entities to be updated.</param>
    /// <param name="autoSave">
    /// Set true to automatically save changes to database.
    /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.</param>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    Task UpdateManyAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false);

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="entity">Entity to be deleted</param>
    /// <param name="autoSave">
    /// Set true to automatically save changes to database.
    /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
    /// </param>
    Task DeleteAsync([NotNull] TEntity entity, bool autoSave = false);

    /// <summary>
    /// Deletes multiple entities.
    /// </summary>
    /// <param name="entities">Entities to be deleted.</param>
    /// <param name="autoSave">
    /// Set true to automatically save changes to database.
    /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
    /// </param>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    Task DeleteManyAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false);

    /// <summary>
    /// Get an existing entity.
    /// </summary>
    /// This is useful for ORMs / database APIs those only save changes with an explicit method call, but you need to immediately save changes to the database.
    /// </param>
    /// <param name="id">id of TEntity</param>
    Task<TEntity?> GetByIdAsync([NotNull] int id);

    /// <summary>
    /// Get current db context using
    /// </summary>
    /// <returns>DbContext ef core</returns>
    DbContext GetContext();
}
