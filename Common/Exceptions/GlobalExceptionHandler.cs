using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace dotnet_Warehouse_Management_System.Common.Exceptions
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // Log dell'eccezione con arricchimento dei dati
            logger.LogError(exception, "Request {Method} {Path} failed with {StatusCode}", httpContext.Request.Method, httpContext.Request.Path, httpContext.Response.StatusCode);

            // Mapping dell'eccezione allo Status Code e al Titolo
            var (status, title, detail) = exception switch
            {
                DbUpdateException dbEx
                    when dbEx.InnerException is SqlException sqlEx
                    && (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                    => (
                        StatusCodes.Status409Conflict,
                        "Duplicate resource",
                        "A customer with the same TaxCode already exists."
                    ),

                KeyNotFoundException
                    => (
                        StatusCodes.Status404NotFound,
                        "Resource not found",
                        exception.Message
                    ),

                ArgumentException
                    => (
                        StatusCodes.Status400BadRequest,
                        "Invalid request",
                        exception.Message
                    ),

                _ =>
                    (
                        StatusCodes.Status500InternalServerError,
                        "Unexpected error",
                        "An unexpected error occurred."
                    )
            };

            // Creazione dei ProblemDetails (Standard RFC 7807)
            var problemDetails = new ProblemDetails
            {
                Type = $"https://httpstatuses.com/{status}",
                Title = title,
                Status = status,
                Detail = detail,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            };

            // Aggiunta del TraceID per il debugging
            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

            // Configurazione della risposta
            httpContext.Response.StatusCode = status;

            // L'estensione WriteAsJsonAsync gestisce automaticamente il Content-Type "application/problem+json"
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true; // Indica che l'eccezione è stata gestita correttamente
        }
    }
}