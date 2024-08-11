using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStoreBackEnd.ExceptionHandler
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> logger;
        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            this.logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred.",
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}

//using Microsoft.AspNetCore.Diagnostics;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;

//namespace BookStoreBackEnd.ExceptionHandler
//{
//    public class ExceptionHandler : IExceptionHandler
//    {
//        private readonly ILogger<ExceptionHandler> logger;

//        public ExceptionHandler(ILogger<ExceptionHandler> logger)
//        {
//            this.logger = logger;
//        }

//        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
//        {
//            logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

//            var statusCode = StatusCodes.Status500InternalServerError;
//            string title = "An unexpected error occurred.";

//            // Handle specific exceptions
//            if (exception is SendGrid.Helpers.Errors.Model.UnauthorizedException)
//            {
//                statusCode = StatusCodes.Status401Unauthorized;
//                title = "Unauthorized";
//            }
//            else if (exception is SendGrid.Helpers.Errors.Model.ForbiddenException)
//            {
//                statusCode = StatusCodes.Status403Forbidden;
//                title = "Forbidden";
//            }

//            var problemDetails = new ProblemDetails
//            {
//                Status = statusCode,
//                Title = title,
//                Detail = exception.Message, // Consider removing this in production for security reasons
//                Instance = httpContext.Request.Path
//            };

//            httpContext.Response.StatusCode = statusCode;
//            httpContext.Response.ContentType = "application/json";

//            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

//            return true;
//        }
//    }
//}
