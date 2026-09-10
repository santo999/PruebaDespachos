using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Despachos.Application.Common.Exceptions;

namespace Despachos.Api.Middleware;


public sealed class AppExceptionHandler(ILogger<AppExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken ct)
    {

        var (statusCode, title) = exception switch
        {
            RepuestoNoEncontradoException => (StatusCodes.Status404NotFound, "Repuesto no encontrado"),
            CantidadInvalidaException => (StatusCodes.Status400BadRequest, "Cantidad inválida"),
            StockInsuficienteException => (StatusCodes.Status409Conflict, "Stock insuficiente"),
            ReferenciaExternaDuplicadaException => (StatusCodes.Status409Conflict, "Solicitud ya procesada"),
            _ => (StatusCodes.Status500InternalServerError, "Error interno")
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, "Error no controlado en {Method} {Path}",
                httpContext.Request.Method, httpContext.Request.Path);
        }
        else
        {
            logger.LogWarning("Solicitud rechazada ({StatusCode}) {Method} {Path}: {Mensaje}",
                statusCode, httpContext.Request.Method, httpContext.Request.Path, exception.Message);
        }

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = statusCode == StatusCodes.Status500InternalServerError
                ? "Ocurrió un error inesperado. Intente nuevamente más tarde."
                : exception.Message,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problem, ct);

        return true;
    }
}
