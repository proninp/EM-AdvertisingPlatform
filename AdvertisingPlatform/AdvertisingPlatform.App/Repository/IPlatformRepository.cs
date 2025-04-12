using AdvertisingPlatform.App.Models;

namespace AdvertisingPlatform.App.Repository;

/// <summary>
/// Интерфейс, представляющий репозиторий платформ и локаций.
/// </summary>
public interface IPlatformRepository
{
    LocationTrieNode Node { get; }
}