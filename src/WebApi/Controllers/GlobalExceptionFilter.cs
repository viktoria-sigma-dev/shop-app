using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UsersApp.src.Application.Services
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is KeyNotFoundException)
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
            else if (context.Exception is BadHttpRequestException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
            else
            {
                context.Result = new ObjectResult("An unexpected error occurred")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            context.ExceptionHandled = true;
        }
    }
}