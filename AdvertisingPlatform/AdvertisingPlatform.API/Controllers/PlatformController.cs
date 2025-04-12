using AdvertisingPlatform.App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingPlatform.API.Controllers;

/// <summary>
/// Контроллер для работы с рекламными платформами.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PlatformController : ControllerBase
{
    private readonly IAdvService _advService;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="PlatformController"/>.
    /// </summary>
    /// <param name="advService">Сервис для работы с рекламными платформами.</param>
    public PlatformController(IAdvService advService)
    {
        _advService = advService;
    }

    /// <summary>
    /// Загружает платформы из текстового файла и добавляет их в систему.
    /// </summary>
    /// <param name="file">Текстовый файл с данными платформ.</param>
    /// <returns>Возвращает <see cref="IActionResult"/> с результатом операции.</returns>
    /// <response code="200">Платформы успешно загружены.</response>
    /// <response code="400">Файл не указан или он имеет неверный формат.</response>
    /// <response code="500">Произошла ошибка на сервере.</response>
    [HttpPost("AddPlatforms")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadPlatformsFromFile(IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Файл не указан");

        if (!file.ContentType.StartsWith("text/"))
            return BadRequest("Можно передавать только текстовые файлы");

        var lines = new List<string>();

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync(cancellationToken);
                if (!string.IsNullOrWhiteSpace(line))
                    lines.Add(line);
            }
        }

        foreach (var line in lines)
            _advService.AddPlatform(line);

        return Ok();
    }

    /// <summary>
    /// Получает список платформ по заданной локации.
    /// </summary>
    /// <param name="location">Локация для поиска платформ.</param>
    /// <returns>Возвращает список платформ, соответствующих локации.</returns>
    /// <response code="200">Платформы успешно найдены.</response>
    /// <response code="400">Запрос некорректен (например, пустое значение локации).</response>
    /// <response code="404">Не найдено платформ для указанной локации.</response>
    /// <response code="500">Произошла ошибка на сервере.</response>
    [HttpGet("GetPlatforms")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<List<string>> GetPlatforms(string location)
    {
        var platforms = _advService.GetPlatforms(location);

        if (platforms.Count() == 0)
            return NotFound("Не найдено рекламных площадок по заданной локации");

        return Ok(platforms.ToList());
    }
}