using System.Net;
using System.Text.Json;

namespace Nota.Api.Middlewares;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Default values
        var statusCode = HttpStatusCode.InternalServerError;
        var errorCode = "INTERNAL_SERVER_ERROR";
        var message = "An unexpected error occurred. Please try again later.";

        switch (exception)
        {
            case KeyNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorCode = "NOT_FOUND";
                message = exception.Message;
                break;

            case ArgumentNullException:
            case NullReferenceException:
                statusCode = HttpStatusCode.BadRequest;
                errorCode = "NULL_REFERENCE";
                message = "A required value was missing.";
                break;

            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                errorCode = "UNAUTHORIZED";
                message = "You do not have permission to perform this action.";
                break;

            case InvalidOperationException:
                statusCode = HttpStatusCode.BadRequest;
                errorCode = "INVALID_OPERATION";
                message = exception.Message;
                break;

            default:
                message = _env.IsDevelopment() ? exception.Message : message;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            ErrorCode = errorCode,
            Message = message,
            Details = _env.IsDevelopment() ? exception.StackTrace : null,
            InnerException = _env.IsDevelopment() ? exception.InnerException?.Message : null
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}