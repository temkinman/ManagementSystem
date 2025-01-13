namespace Catalog.Api.Middlewares;

public class TaskCancellationHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TaskCancellationHandlerMiddleware> _logger;

    public TaskCancellationHandlerMiddleware(RequestDelegate next, ILogger<TaskCancellationHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex) when (ex is TaskCanceledException or OperationCanceledException)
        {
            _logger.LogInformation("Task cancelled");
        }
    }
}