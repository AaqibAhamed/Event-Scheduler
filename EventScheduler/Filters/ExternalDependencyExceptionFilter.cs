using EventScheduler.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventScheduler.Filters;

public class ExternalDependencyExceptionFilter : IActionFilter, IOrderedFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
       
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is ExternalDependencyException exception)
        {
            context.Result = new ObjectResult(exception.Payload)
            {
                StatusCode = (int)exception.StatusCode,
            };
            
            context.ExceptionHandled = true; // important 
        }
    }

    public int Order =>int.MaxValue;
}