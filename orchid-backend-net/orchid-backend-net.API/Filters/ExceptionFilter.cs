﻿using System.Diagnostics;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using orchid_backend_net.Application.Common.Exceptions;
using orchid_backend_net.Domain.Common.Exceptions;

namespace orchid_backend_net.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ValidationException exception:
                    foreach (var error in exception.Errors)
                    {
                        context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    context.Result = new BadRequestObjectResult(new ValidationProblemDetails(context.ModelState))
                    .AddContextInformation(context);
                    context.ExceptionHandled = true;
                    break;
                case ForbiddenAccessException:
                    context.Result = new ForbidResult();
                    context.ExceptionHandled = true;
                    break;
                case UnauthorizedAccessException:
                    context.Result = new ForbidResult();
                    context.ExceptionHandled = true;
                    break;
                case NotFoundException exception:
                    context.Result = new NotFoundObjectResult(new ProblemDetails
                    {
                        Detail = exception.Message
                    })
                    .AddContextInformation(context);
                    context.ExceptionHandled = true;
                    break;
                case IncorrectPasswordException exception:
                    context.Result = new BadRequestObjectResult(new ProblemDetails
                    {
                        Detail = exception.Message
                    }).AddContextInformation(context);
                    context.ExceptionHandled = true;
                    break;
            }
        }
    }

    internal static class ProblemDetailsExtensions
    {
        public static IActionResult AddContextInformation(this ObjectResult objectResult, ExceptionContext context)
        {
            if (objectResult.Value is not ProblemDetails problemDetails)
            {
                return objectResult;
            }
            problemDetails.Extensions.Add("traceId", Activity.Current?.Id ?? context.HttpContext.TraceIdentifier);
            problemDetails.Type = "https://httpstatuses.io/" + (objectResult.StatusCode ?? problemDetails.Status);
            return objectResult;
        }
    }
}
