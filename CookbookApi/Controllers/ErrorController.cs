using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CookbookApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var stackTrace = context?.Error.StackTrace;
            var errorMessage = context?.Error.Message;
            
            // Log these details for troubleshooting purposes
            // ToDo: Add Logger
            Console.WriteLine($"Error: {errorMessage}, Stack Trace: {stackTrace}");
            
            return Problem();
        }
    }
}
