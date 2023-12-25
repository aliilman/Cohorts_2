using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        // Action adını al
        string actionName = context.GetEndpoint()?.DisplayName;

        // Loglama işlemi
        Console.WriteLine(($"Action '{actionName}' is invoked at {DateTime.Now}"));

        // Sonraki middleware'e geç
        await _next(context);

        await _next.Invoke(context);
    }
}
