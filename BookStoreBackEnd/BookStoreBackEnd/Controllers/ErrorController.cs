using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStoreBackEnd.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [HttpGet]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (context != null)
            {
                var exception = context.Error; // This is the exception your middleware caught
                return Problem(
                    detail: exception.Message,
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    title: "An unexpected error occurred."
                );
            }

            return Problem();
        }
    }

}
