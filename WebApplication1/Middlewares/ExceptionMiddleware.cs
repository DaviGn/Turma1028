using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Web.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            if(typeof(ICustomException).IsAssignableFrom(ex.GetType()))
            {
                var exInterface = ex as ICustomException;
                context.Response.StatusCode = exInterface.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(exInterface.GetResponse());
            }
        }
    }
}
