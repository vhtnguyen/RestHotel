using System.Net;
using Hotel.Shared.Exceptions;

namespace Hotel.API.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (DomainException ex)
        {
            _logger.LogInformation($"domain layer throw exception {ex.Message}");
            // write data
            await context.Response.WriteAsJsonAsync(new
            {
                Message = ex.Message,
                Code = ex.Code,
                States = ex.HttpStatusCode
            });
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"server throw exception {ex.Message}");
            await context.Response.WriteAsJsonAsync(new
            {
                Message = ex.Message,
                Code = "Server Throw Exception",
                States = HttpStatusCode.InternalServerError
            });
        }
    }
}
