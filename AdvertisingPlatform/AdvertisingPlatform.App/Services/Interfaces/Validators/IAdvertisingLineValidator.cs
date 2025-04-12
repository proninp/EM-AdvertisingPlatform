namespace AdvertisingPlatform.App.Services.Interfaces.Validators;

/// <summary>
/// Интерфейс валидатора для проверки строки, содержащей информацию
/// о рекламной платформе и связанных с ней локациях.
/// </summary>
public interface IAdvertisingLineValidator
{
    /// <summary>
    /// Выполняет базовую валидацию строки, представляющей описание рекламной платформы и локаций.
    /// </summary>
    /// <param name="line">Исходная строка, подлежащая проверке.</param>
    /// <exception cref="AdvertisingLineValidationException">
    /// Выбрасывается, если строка пустая или содержит только пробельные символы.
    /// </exception>
    void Validate(string line);
}