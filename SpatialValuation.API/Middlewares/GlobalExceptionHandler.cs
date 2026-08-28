using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SpatialValuation.Api.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        httpContext.Response.ContentType = "application/problem+json";

        switch (exception)
        {
            case ValidationException validationException:
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                var validationProblem = new HttpValidationProblemDetails(
                    validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()))
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Failure",
                    Detail = "One or more spatial or input validation rules were violated.",
                    Instance = httpContext.Request.Path
                };

                await httpContext.Response.WriteAsJsonAsync(validationProblem, cancellationToken);
                break;

            case DbUpdateException dbUpdateException:
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

                var dbProblem = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Database Constraint or Spatial Exception",
                    Detail = "A database constraint failure or invalid PostGIS geometry operation occurred.",
                    Instance = httpContext.Request.Path
                };

                await httpContext.Response.WriteAsJsonAsync(dbProblem, cancellationToken);
                break;

            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var serverErrorProblem = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while processing the request.",
                    Instance = httpContext.Request.Path
                };

                await httpContext.Response.WriteAsJsonAsync(serverErrorProblem, cancellationToken);
                break;
        }

        return true;
    }
}