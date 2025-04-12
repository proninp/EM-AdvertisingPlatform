using AdvertisingPlatform.App.Exceptions;
using AdvertisingPlatform.App.Services.Interfaces.Validators;

namespace AdvertisingPlatform.App.Services.Validators;

/// <summary>
/// Реализация <see cref="IAdvertisingLineValidator"/>, выполняющая базовую проверку строки
/// с описанием рекламной платформы и локаций на предмет пустого значения.
/// </summary>
public class AdvertisingLineValidator : IAdvertisingLineValidator
{
    /// <inheritdoc />
    public void Validate(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            throw new AdvertisingLineValidationException(
                "Строка соотношения платформы и локаций не может быть пустой", nameof(line));
    }
}