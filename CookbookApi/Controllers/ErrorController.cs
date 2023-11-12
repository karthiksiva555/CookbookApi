using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CookbookApi.Controllers;

[ApiController]
public class ErrorController : ControllerBase
{
    [HttpGet("/error")]
    public IActionResult Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var stackTrace = context?.Error.StackTrace;
        var errorMessage = context?.Error.Message;
            
        // Log these details for troubleshooting purposes
            
        return Problem();
    }
}