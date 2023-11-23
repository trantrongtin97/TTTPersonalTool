namespace TTT.Framework.EfCore;

public interface IEntity
{
}
/// <summary>
/// Defines an entity with a single primary key with "ID" property.
/// </summary>
/// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
public interface IEntity<TKey> : IEntity
{
    /// <summary>
    /// Unique identifier for this entity.
    /// </summary>
    TKey Id { get; }
}
