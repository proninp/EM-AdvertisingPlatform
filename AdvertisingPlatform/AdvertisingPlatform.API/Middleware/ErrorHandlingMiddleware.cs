namespace AdvertisingPlatform.API.Middleware;

using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using AdvertisingPlatform.App.Exceptions;
using Serilog;
using Serilog.Context;

/// <summary>
/// Middleware для обработки ошибок в HTTP-запросах.
/// Перехватывает необработанные исключения и записывает их в журнал. Также формирует ответ с ошибкой для клиента.
/// </summary>
public sealed class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ErrorHandlingMiddleware"/>.
    /// </summary>
    /// <param name="next">Делегат для передачи управления следующему компоненту в пайплайне обработки HTTP-запроса.</param>
    /// <param name="logger">Объект для записи логов ошибок.</param>
    public ErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Выполняет обработку HTTP-запроса и перехватывает исключения, которые могут произойти в процессе.
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    /// <exception cref="Exception">Если происходит необработанное исключение, оно будет перехвачено и обработано.</exception>
    public async Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Произошла необработанная ошибка");
                await HandleExceptionAsync(context, ex);
            }
        }
    }

    /// <summary>
    /// Обрабатывает исключение, устанавливая соответствующий код статуса и возвращая подробности об ошибке в ответе.
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса.</param>
    /// <param name="exception">Исключение, которое необходимо обработать.</param>
    /// <returns>Задача, представляющая асинхронную операцию записи ответа.</returns>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            AdvertisingLineValidationException => StatusCodes.Status400BadRequest,
            LocationValidationException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var result = JsonSerializer.Serialize(new
        {
            error = "Internal Server Error",
            detail = exception.Message,
            traceId = context.TraceIdentifier
        });

        return context.Response.WriteAsync(result);
    }
}