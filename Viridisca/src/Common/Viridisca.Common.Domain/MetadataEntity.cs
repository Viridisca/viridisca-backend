namespace Viridisca.Common.Domain;

/// <summary>
/// Extensions for entities to allow storing and retrieving metadata
/// </summary>
public static class MetadataEntityExtensions
{
    private static readonly Dictionary<Entity, Dictionary<string, object>> _metadata = new();
    
    /// <summary>
    /// Sets metadata value for the entity
    /// </summary>
    /// <param name="entity">Entity to store metadata on</param>
    /// <param name="key">Metadata key</param>
    /// <param name="value">Metadata value</param>
    public static void SetMetadata(this Entity entity, string key, object value)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        if (string.IsNullOrEmpty(key))
            throw new ArgumentException("Key cannot be null or empty", nameof(key));
            
        if (!_metadata.ContainsKey(entity))
        {
            _metadata[entity] = [];
        }
        
        _metadata[entity][key] = value;
    }
    
    /// <summary>
    /// Tries to get metadata value for the entity
    /// </summary>
    /// <typeparam name="T">Type of the metadata value</typeparam>
    /// <param name="entity">Entity to get metadata from</param>
    /// <param name="key">Metadata key</param>
    /// <param name="value">Metadata value if found</param>
    /// <returns>True if metadata value was found and successfully converted to T, false otherwise</returns>
    public static bool TryGetMetadata<T>(this Entity entity, string key, out T value)
    {
        value = default;
        
        if (entity == null || string.IsNullOrEmpty(key))
            return false;
            
        if (!_metadata.TryGetValue(entity, out var entityMetadata))
            return false;
            
        if (!entityMetadata.TryGetValue(key, out var objValue))
            return false;
            
        if (objValue is T typedValue)
        {
            value = typedValue;
            return true;
        }
        
        return false;
    }
    
    /// <summary>
    /// Removes metadata for the entity
    /// </summary>
    /// <param name="entity">Entity to clear metadata for</param>
    /// <param name="key">Optional key to remove. If not specified, all metadata for the entity will be removed</param>
    public static void ClearMetadata(this Entity entity, string key = null)
    {
        if (entity == null)
            return;
            
        if (string.IsNullOrEmpty(key))
        {
            _metadata.Remove(entity);
        }
        else if (_metadata.TryGetValue(entity, out var entityMetadata))
        {
            entityMetadata.Remove(key);
            
            if (entityMetadata.Count == 0)
            {
                _metadata.Remove(entity);
            }
        }
    }
}
