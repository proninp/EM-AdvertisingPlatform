namespace AdvertisingPlatform.App.Services.Interfaces.Validators;

/// <summary>
/// Интерфейс валидатора, отвечающего за проверку структуры строки, содержащей информацию о платформе и связанных локациях.
/// </summary>
public interface IPlatformLocationsValidator
{
    /// <summary>
    /// Выполняет валидацию массива, полученного после разделения строки платформы и локаций.
    /// </summary>
    /// <param name="line">Исходная строка, содержащая данные о платформе и локациях.</param>
    /// <param name="advertisingArray">
    /// Массив строк, полученный в результате разделения исходной строки по символу-разделителю ':'.
    /// </param>
    /// <exception cref="AdvertisingLineValidationException">
    /// Выбрасывается, если разделение строки прошло некорректно:
    /// - отсутствует разделитель,
    /// - больше одного разделителя.
    /// </exception>
    void Validate(string line, string[]? advertisingArray);
}