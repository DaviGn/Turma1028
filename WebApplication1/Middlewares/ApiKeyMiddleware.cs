using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Web.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var apiKeyHeader = context.Request.Headers["X-Key"];

        if (apiKeyHeader.Count == 0 || apiKeyHeader[0] != "TransformaDev")
        {
            context.Response.StatusCode = 401;
            return;
        }

        await _next(context);
    }
}
