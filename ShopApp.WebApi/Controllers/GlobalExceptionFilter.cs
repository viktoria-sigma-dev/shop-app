using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopApp.Application.Exceptions;
using ShopApp.Domain.Exceptions;

namespace ShopApp.Application.Services
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private static readonly Dictionary<Type, int> ExceptionStatusCodes = new()
        {
            { typeof(OrderNotFoundException), StatusCodes.Status404NotFound },
            { typeof(UserNotFoundException), StatusCodes.Status404NotFound },
            { typeof(ProductNotFoundException), StatusCodes.Status404NotFound },
            { typeof(ProductAlreadyExistsException), StatusCodes.Status400BadRequest },
            { typeof(InvalidUserValueException), StatusCodes.Status400BadRequest },
            { typeof(ProductQuantityExceededException), StatusCodes.Status400BadRequest },
            { typeof(NegativeProductPriceException), StatusCodes.Status400BadRequest },
            { typeof(NegativeProductAmountException), StatusCodes.Status400BadRequest },
            { typeof(ProductInvalidQuantityException), StatusCodes.Status400BadRequest },
            { typeof(ProductNotFoundInOrderException), StatusCodes.Status400BadRequest },
            { typeof(InvalidOrderStatusException), StatusCodes.Status400BadRequest },
            { typeof(InvalidUserBalanceException), StatusCodes.Status400BadRequest },
            { typeof(InsufficientUserBalanceException), StatusCodes.Status400BadRequest },
            { typeof(InsufficientProductAmountException), StatusCodes.Status400BadRequest },
            { typeof(PaidTransactionNotFoundException), StatusCodes.Status400BadRequest }
        };
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine($"[GlobalExceptionFilter] {context.Exception.GetType().Name}: {context.Exception.Message}");

            if (ExceptionStatusCodes.TryGetValue(context.Exception.GetType(), out var statusCode))
            {
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = statusCode
                };
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