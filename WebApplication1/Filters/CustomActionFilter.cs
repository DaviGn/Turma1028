using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Web.Filters;

public class CustomActionFilterAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Executando antes da action");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("Executando depois da action");
    }
}
