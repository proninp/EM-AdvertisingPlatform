namespace AdvertisingPlatform.App.Services.Interfaces.Validators;

/// <summary>
/// Интерфейс валидатора, выполняющего проверку названия рекламной платформы в строке с информацией о платформе и локациях.
/// </summary>
public interface IPlatformValidator
{
    /// <summary>
    /// Валидирует название рекламной платформы.
    /// </summary>
    /// <param name="line">Исходная строка, содержащая данные о платформе и локациях.</param>
    /// <param name="platform">Название рекламной платформы, выделенное из строки.</param>
    /// <exception cref="AdvertisingLineValidationException">
    /// Выбрасывается, если название платформы отсутствует или состоит только из пробелов.
    /// </exception>
    void Validate(string line, string platform);
}