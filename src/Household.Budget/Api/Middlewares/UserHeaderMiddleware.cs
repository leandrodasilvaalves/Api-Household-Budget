using System.Text.Json;
using System.Text.Json.Serialization;

using Household.Budget.Contracts.Errors;
using Household.Budget.Contracts.Http.Responses;

namespace Household.Budget.Api.Controllers.Middlewares;

public class UserHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public UserHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.ContainsKey(KnownHeaders.UserHeader))
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json; charset=utf-8";
            await context.Response.WriteAsync(ResponseError());
            return;
        }
        await _next(context);
    }

    private static string ResponseError()
    {
        var response = new Response<object>(KnownErrors.USER_ID_IS_REQUIRED);
        var jsonOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
        return JsonSerializer.Serialize(response, jsonOptions);
    }
}

public static class UserHeaderMiddlewareExtensions
{
    public static IApplicationBuilder UserHeader(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserHeaderMiddleware>();
    }
}
