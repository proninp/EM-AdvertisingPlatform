namespace AdvertisingPlatform.App.Services.Interfaces.Validators;

/// <summary>
/// Интерфейс валидатора, выполняющего проверку массива локаций,
/// извлечённого из строки описания рекламной платформы.
/// </summary>
public interface ILocationsLineValidator
{
    /// <summary>
    /// Выполняет проверку на наличие и корректность локаций.
    /// </summary>
    /// <param name="line">Исходная строка, содержащая информацию о платформе и локациях (для сообщения об ошибке).</param>
    /// <param name="locations">Массив строк, представляющих локации, подлежащие проверке.</param>
    /// <exception cref="AdvertisingLineValidationException">
    /// Выбрасывается, если массив локаций пустой, отсутствует, или содержит пустые/некорректные значения.
    /// </exception>
    void Validate(string line, string[]? locations);
}