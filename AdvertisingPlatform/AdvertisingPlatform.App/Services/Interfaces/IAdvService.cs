namespace AdvertisingPlatform.App.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса для управления информацией о рекламных платформах и их привязке к локациям.
/// </summary>
public interface IAdvService
{
    /// <summary>
    /// Метод добавления информации о рекламной площадке и локациях
    /// </summary>
    /// <param name="platformInfoLine">
    /// Строка, представляющая собой описание рекламной платформы и связанных с ней локаций в следующем формате:
    /// <c>[НазваниеПлатформы:/локация1,/локация2,...]</c>.
    /// Пример: <c>"Яндекс.Директ:/ru,/ru/moscow"</c>.
    /// </param>
    public void AddPlatform(string platformInfoLine);

    /// <summary>
    /// Метод поиска списка рекламных площадок для заданной локации
    /// </summary>
    /// <param name="location">Локация по которой осуществляется поиск</param>
    /// <returns>
    /// Список имён рекламных платформ, действующих на указанной или вложенной локации.
    /// </returns>
    public IEnumerable<string> GetPlatforms(string location);
}