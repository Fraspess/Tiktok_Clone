using SixLabors.ImageSharp;

namespace Tiktok_Clone.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnknownImageFormatException ex)
            {
                _logger.LogWarning("Invalid image format : {error} ", ex.Message);

            }
            catch (Exception ex)
            {
                _logger.LogError("An unhandled exception occurred : {error} ", ex.Message);

            }
        }
    }
}
