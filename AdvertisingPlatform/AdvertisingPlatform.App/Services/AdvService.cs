using AdvertisingPlatform.App.Models;
using AdvertisingPlatform.App.Repository;
using AdvertisingPlatform.App.Services.Interfaces;
using AdvertisingPlatform.App.Services.Interfaces.Providers;

namespace AdvertisingPlatform.App.Services;

/// <summary>
/// Сервис для добавления информации о рекламных платформах и получения платформ по локации.
/// Использует структуру данных <c>Trie</c> для хранения и быстрого поиска.
/// </summary>
public sealed class AdvService : IAdvService
{
    private readonly IPlatformRepository _repository;
    private readonly ILocationsProvider _locationsProvider;
    private readonly IAdvertisingInfoProvider _advertisingInfoProvider;

    /// <summary>
    /// Создает экземпляр <see cref="AdvService"/> с внедрением зависимостей.
    /// </summary>
    /// <param name="repository">Репозиторий для хранения платформ по структуре Trie.</param>
    /// <param name="locationsProvider">Провайдер для разбиения строки локации на части.</param>
    /// <param name="advertisingInfoProvider">Провайдер, извлекающий валидированную информацию из строки описания платформы.</param>
    public AdvService(
        IPlatformRepository repository,
        ILocationsProvider locationsProvider,
        IAdvertisingInfoProvider advertisingInfoProvider)
    {
        _repository = repository;
        _locationsProvider = locationsProvider;
        _advertisingInfoProvider = advertisingInfoProvider;
    }

    /// <inheritdoc/>
    public void AddPlatform(string platformInfoLine)
    {
        var platformInfo = _advertisingInfoProvider.Provide(platformInfoLine);

        foreach (var location in platformInfo.Locations)
        {
            var locationParts = _locationsProvider.Provide(location);

            var node = _repository.Node;

            foreach (var part in locationParts)
            {
                node.Children.TryAdd(part, new LocationTrieNode());
                node = node.Children[part];
            }
            node.Platforms.Add(platformInfo.Platform);
        }
    }

    /// <inheritdoc/>
    public IEnumerable<string> GetPlatforms(string location)
    {
        var locationParts = _locationsProvider.Provide(location);

        var node = _repository.Node;
        var result = new HashSet<string>();

        foreach (var part in locationParts)
        {
            if (node.Children.TryGetValue(part, out node))
            {
                if (node.Platforms.Count > 0)
                    result.UnionWith(node.Platforms);
            }

            if (node is null)
                break;
        }
        return result;
    }
}