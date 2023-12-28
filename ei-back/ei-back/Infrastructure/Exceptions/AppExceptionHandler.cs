using Microsoft.AspNetCore.Diagnostics;

namespace ei_back.Infrastructure.Exceptions
{
    public class AppExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<AppExceptionHandler> _logger;

        public AppExceptionHandler(ILogger<AppExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"Something went wrong: {exception.GetType().Name}. Message: {exception.Message}. StackTrace: {exception.StackTrace}");
            
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsync("Something went wrong", cancellationToken);

            return true;
        }
    }
}
