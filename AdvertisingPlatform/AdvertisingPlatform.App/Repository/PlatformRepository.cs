using AdvertisingPlatform.App.Models;

namespace AdvertisingPlatform.App.Repository;

/// <summary>
/// Реализация репозитория платформ и локаций.
/// </summary>
public sealed class PlatformRepository : IPlatformRepository
{
    private readonly LocationTrieNode _node;

    /// <summary>
    /// Получает корневой узел структуры данных для локации.
    /// </summary>
    public LocationTrieNode Node => _node;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="PlatformRepository"/> 
    /// с созданием нового корневого узла <see cref="LocationTrieNode"/>.
    /// </summary>
    public PlatformRepository()
    {
        _node = new LocationTrieNode();
    }
}