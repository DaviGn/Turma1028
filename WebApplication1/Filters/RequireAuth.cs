using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Web.Filters;

public class RequireAuth : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var apiKeyHeader = context.HttpContext.Request.Headers["X-Key"];

        if (apiKeyHeader.Count == 0 || apiKeyHeader[0] != "TransformaDev")
        {
            context.Result = new UnauthorizedObjectResult(new
            {
                message = "Unathorized"
            });
        }
    }
}
