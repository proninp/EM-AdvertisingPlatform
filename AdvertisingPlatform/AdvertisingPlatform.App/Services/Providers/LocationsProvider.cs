using AdvertisingPlatform.App.Services.Interfaces.Providers;
using AdvertisingPlatform.App.Services.Interfaces.Validators;

namespace AdvertisingPlatform.App.Services.Providers;

/// <summary>
/// Реализация <see cref="ILocationsProvider"/>, выполняющая валидацию и разбиение строки локации на части.
/// </summary>
public class LocationsProvider : ILocationsProvider
{
    private readonly ILocationValidator _locationValidator;

    /// <summary>
    /// Создаёт новый экземпляр <see cref="LocationsProvider"/>.
    /// </summary>
    /// <param name="locationValidator">Валидатор строки локации.</param>
    public LocationsProvider(ILocationValidator locationValidator)
    {
        _locationValidator = locationValidator;
    }

    /// <inheritdoc/>
    public string[] Provide(string locationText)
    {
        _locationValidator.Validate(locationText);
        return locationText.Split('/', StringSplitOptions.RemoveEmptyEntries);
    }
}