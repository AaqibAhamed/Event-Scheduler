using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EventScheduler.Controllers;

[Route("[controller]")]
[ApiController]
[AllowAnonymous]
[ApiExplorerSettings(IgnoreApi = true)]

public class ErrorHandlerController : ControllerBase
{
    [Route("error")]
    public IActionResult HandleError() => Problem();

    //for development env
    [Route("error-development")]
    public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }
        
        var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerFeature>();

        return Problem(
            detail: exceptionHandler.Error.InnerException?.StackTrace,
            title: exceptionHandler.Error.Message);
    }
}