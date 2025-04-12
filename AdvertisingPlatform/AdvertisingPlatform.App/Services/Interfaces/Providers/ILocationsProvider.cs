namespace AdvertisingPlatform.App.Services.Interfaces.Providers;

/// <summary>
/// Интерфейс для предоставления частей локации на основе её текстового представления.
/// </summary>
public interface ILocationsProvider
{
    /// <summary>
    /// Разбивает строку локации на составляющие части пути.
    /// </summary>
    /// <param name="locationText">Локация в виде строки, например <c>"/ru/moscow"</c>.</param>
    /// <returns>Массив строк, содержащий отдельные части локации, например <c>["ru", "moscow"]</c>.</returns>
    string[] Provide(string locationText);
}