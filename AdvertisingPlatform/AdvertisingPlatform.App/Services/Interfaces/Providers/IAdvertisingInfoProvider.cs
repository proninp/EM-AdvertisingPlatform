using AdvertisingPlatform.App.Models;

namespace AdvertisingPlatform.App.Services.Interfaces.Providers;

/// <summary>
/// Интерфейс провайдера для разбора и валидации строки, содержащей информацию о рекламной платформе и связанных с ней локациях.
/// </summary>
public interface IAdvertisingInfoProvider
{
    /// <summary>
    /// Выполняет разбор и валидацию строки с информацией о рекламной платформе и локациях.
    /// </summary>
    /// <param name="line">
    /// Строка в формате <c>Платформа:/локация1,/локация2,...</c>. 
    /// Пример: <c>"Яндекс.Директ:/ru,/ru/moscow"</c>.
    /// </param>
    /// <returns>
    /// Объект <see cref="AdvertisingInfo"/>, содержащий валидированное название платформы и массив локаций.
    /// </returns>
    AdvertisingInfo Provide(string line);
}