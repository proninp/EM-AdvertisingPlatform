using AdvertisingPlatform.App.Exceptions;
using AdvertisingPlatform.App.Services.Interfaces.Validators;

namespace AdvertisingPlatform.App.Services.Validators;

/// <summary>
/// Реализация <see cref="ILocationsLineValidator"/>, выполняющая
/// проверку корректности формата переданной строки, представляющей локацию.
/// </summary>
public class LocationsLineValidator : ILocationsLineValidator
{
    /// <inheritdoc />
    public void Validate(string line, string[]? locations)
    {
        if (locations is null || locations.Length == 0)
            throw new AdvertisingLineValidationException(
                $"Строка '{line}' не содержит локаций", nameof(locations));

        if (locations.Any(string.IsNullOrWhiteSpace))
            throw new AdvertisingLineValidationException($"В строке '{line}' не должно быть пустых локаций", nameof(line));
    }
}