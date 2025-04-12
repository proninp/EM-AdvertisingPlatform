namespace AdvertisingPlatform.App.Services.Interfaces.Validators;

/// <summary>
/// Интерфейс валидатора для проверки корректности формата отдельной локации.
/// </summary>
public interface ILocationValidator
{
    /// <summary>
    /// Выполняет валидацию строки, представляющей локацию.
    /// </summary>
    /// <param name="location">Локация для проверки.</param>
    /// <exception cref="LocationValidationException">
    /// Выбрасывается, если строка не соответствует ожидаемому формату локации.
    /// </exception>
    void Validate(string location);
}