

namespace TTT.Framework.EfCore;

/// <summary>
/// Defines an entity with a single primary key with "Id" property of IEntity.
/// </summary>
/// <typeparam name="TKey">Type of the primary key of the IEntity</typeparam>
public interface IDataEntity<TKey> : IEntity<TKey>
{
    /// <summary>
    /// Requied field for data entity. [MaxLength(DefineFieldValue.String_Lenght_500)]
    /// </summary>
    string? TenantCode { get; set; }
}
