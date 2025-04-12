using AdvertisingPlatform.App.Models;
using AdvertisingPlatform.App.Services.Interfaces.Providers;
using AdvertisingPlatform.App.Services.Interfaces.Validators;

namespace AdvertisingPlatform.App.Services.Providers;

/// <summary>
/// Реализация <see cref="IAdvertisingInfoProvider"/>, обеспечивающая пошаговую валидацию и разбор строки рекламной информации.
/// </summary>
public class AdvertisingInfoProvider : IAdvertisingInfoProvider
{
    private readonly IAdvertisingLineValidator _lineValidator;
    private readonly IPlatformLocationsValidator _platformLocationsValidator;
    private readonly IPlatformValidator _platformValidator;
    private readonly ILocationsLineValidator _locationsLineValidator;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AdvertisingInfoProvider"/> с необходимыми валидаторами.
    /// </summary>
    /// <param name="lineValidator">Валидатор общей структуры строки.</param>
    /// <param name="platformLocationsValidator">Валидатор корректности разделителя между платформой и локациями.</param>
    /// <param name="platformValidator">Валидатор значения платформы.</param>
    /// <param name="locationsLineValidator">Валидатор списка локаций.</param>
    public AdvertisingInfoProvider(
        IAdvertisingLineValidator lineValidator,
        IPlatformLocationsValidator platformLocationsValidator,
        IPlatformValidator platformValidator,
        ILocationsLineValidator locationsLineValidator)
    {
        _lineValidator = lineValidator;
        _platformLocationsValidator = platformLocationsValidator;
        _platformValidator = platformValidator;
        _locationsLineValidator = locationsLineValidator;
    }

    /// <inheritdoc/>
    public AdvertisingInfo Provide(string line)
    {
        _lineValidator.Validate(line);

        var advertisingArray = line.Split(':');

        _platformLocationsValidator.Validate(line, advertisingArray);

        var platform = advertisingArray[0];

        _platformValidator.Validate(line, platform);

        var locations = advertisingArray[1].TrimStart().Split(',');

        _locationsLineValidator.Validate(line, locations);

        return new AdvertisingInfo(platform, locations);
    }
}

