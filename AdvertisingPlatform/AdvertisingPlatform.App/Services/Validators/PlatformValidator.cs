using AdvertisingPlatform.App.Exceptions;
using AdvertisingPlatform.App.Services.Interfaces.Validators;

namespace AdvertisingPlatform.App.Services.Validators;

/// <summary>
/// Реализация <see cref="IPlatformValidator"/>, выполняющая валидацию названия рекламной платформы,
/// извлечённого из строки с данными.
/// </summary>
public class PlatformValidator : IPlatformValidator
{
    /// <inheritdoc />
    public void Validate(string line, string platform)
    {
        if (string.IsNullOrWhiteSpace(platform))
            throw new AdvertisingLineValidationException($"Строка '{line}' не содержит названия платформы", nameof(line));
    }
}