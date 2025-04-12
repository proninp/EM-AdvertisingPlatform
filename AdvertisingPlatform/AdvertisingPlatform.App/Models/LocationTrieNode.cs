using System.Collections.Concurrent;

namespace AdvertisingPlatform.App.Models;

/// <summary>
/// Представляет узел в структуре данных Trie для хранения локаций и платформ.
/// </summary>
public sealed class LocationTrieNode
{
    /// <summary>
    /// Дочерние узлы текущего узла, представляющие дальнейшие уровни в иерархии локаций.
    /// Ключи словаря — это строки, представляющие части локации, а значения — дочерние узлы.
    /// </summary>
    public ConcurrentDictionary<string, LocationTrieNode> Children { get; } = new();

    /// <summary>
    /// Получает коллекцию платформ, ассоциированных с текущим узлом локации.
    /// </summary>
    public ConcurrentBag<string> Platforms { get; } = new();
}