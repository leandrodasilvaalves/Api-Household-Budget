using System.Text.Json;
using System.Text.Json.Serialization;

using Household.Budget.Contracts.Http.Responses;

namespace Household.Budget.Api.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
    private readonly ILogger<ExceptionMiddleware> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly JsonSerializerOptions _jsonOptions = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, WriteIndented = true };

    public async Task InvokeAsync(HttpContext httpCotnext)
    {
        try
        {
            await _next(httpCotnext);
        }
        catch (Exception exception)
        {
            _logger.LogError("An exception occurred in the system. Message: {0} \n. StackTrace: {1}", 
                exception.Message, JsonSerializer.Serialize(exception.StackTrace, _jsonOptions));
                
            httpCotnext.Response.StatusCode = 500;
            httpCotnext.Response.ContentType = "application/json; charset=utf-8";
            await httpCotnext.Response.WriteAsync(JsonSerializer.Serialize(AbstractResponse<object>.UnexpectedError(), _jsonOptions));
        }
    }
}

public static class UserHeaderMiddlewareExtensions
{
    public static IApplicationBuilder UserExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}