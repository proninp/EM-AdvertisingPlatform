namespace AdvertisingPlatform.App.Models;

/// <summary>
/// Представляет информацию о рекламной платформе и связанных с ней локациях.
/// </summary>
public record AdvertisingInfo(string Platform, IEnumerable<string> Locations);