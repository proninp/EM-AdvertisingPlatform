using AdvertisingPlatform.App.Exceptions;
using AdvertisingPlatform.App.Services.Interfaces.Validators;

namespace AdvertisingPlatform.App.Services.Validators;

/// <summary>
/// Реализация <see cref="ILocationValidator"/>, проверяющая строку локации на соответствие базовым правилам формата.
/// </summary>
public class LocationValidator : ILocationValidator
{
    /// <inheritdoc />
    public void Validate(string location)
    {
        if (string.IsNullOrEmpty(location))
            throw new LocationValidationException(
                $"Локация не может быть пустой строкой", nameof(location));

        if (!location.StartsWith("/"))
            throw new LocationValidationException(
                $"Локация должна начинаться с символа '/': \"{location}\"", nameof(location));

        if (location.EndsWith("/"))
            throw new LocationValidationException(
                $"Локация не может оканчиваться символом '/': \"{location}\"", nameof(location));

        if (location.Contains("//"))
            throw new LocationValidationException(
                $"Локация содержит недопустимую последовательность символов '//': \"{location}\"", nameof(location));
    }
}