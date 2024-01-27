using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace Web.Filters;

public class ExceptionFilter : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        // Retornarmos um JSON e definimos o status code como 500 (Internal Server Error)
        context.Result = new ObjectResult(new
        {
            message = "Ops! Ocorreu um erro inesperado"
        })
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        // Marcamos a exceção como tratada
        context.ExceptionHandled = true;
    }
}
