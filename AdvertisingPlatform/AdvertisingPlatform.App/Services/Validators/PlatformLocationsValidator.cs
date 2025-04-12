using AdvertisingPlatform.App.Exceptions;
using AdvertisingPlatform.App.Services.Interfaces.Validators;

namespace AdvertisingPlatform.App.Services.Validators;

/// <summary>
/// Реализация <see cref="IPlatformLocationsValidator"/>, выполняющая валидацию структуры строки,
/// описывающей рекламную платформу и список локаций.
/// </summary>
public class PlatformLocationsValidator : IPlatformLocationsValidator
{
    /// <inheritdoc />
    public void Validate(string line, string[]? advertisingArray)
    {
        if (advertisingArray is null || advertisingArray.Length < 2)
            throw new AdvertisingLineValidationException(
                $"Строка '{line}' не содержит символа-разделителя между локациями и платформой ':'", nameof(line));

        if (advertisingArray.Length > 2)
            throw new AdvertisingLineValidationException(
                $"Строка '{line}' содержит более одного символа-разделителя между локациями и платформой ':'", nameof(line));
    }
}