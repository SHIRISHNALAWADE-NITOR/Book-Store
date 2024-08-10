using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var request = httpContext.Request;
        var logFilePath = "logs/requests_log.txt";
        Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

        // Use FileStream with FileShare to allow other processes to access the file.
        using (var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
        using (var writer = new StreamWriter(fileStream))
        {
            
            await writer.WriteLineAsync($"[{DateTime.Now}] {request.Method} {request.Path}");

            await writer.WriteLineAsync("Headers:");
            foreach (var header in request.Headers)
            {
                await writer.WriteLineAsync($"{header.Key}: {header.Value}");
            }
            await writer.WriteLineAsync("------");

        }

        // Call the next middleware in the pipeline
        await _next(httpContext);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}