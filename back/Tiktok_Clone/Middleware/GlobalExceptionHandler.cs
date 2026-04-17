using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Exceptions;

namespace Tiktok_Clone.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            webHostEnvironment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogInformation("Помилка валідації: {error} ", ex.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(ApiResponse<object>.Error(ex.Message));
            }
            catch (UnauthorizedException ex)
            {
                _logger.LogWarning("Не авторизований : {error} ", ex.Message);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(ApiResponse<object>.Error(ex.Message));
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation("Не знайдено : {error} ", ex.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(ApiResponse<object>.Error(ex.Message));
            }
            catch (NotAllowedException ex)
            {
                _logger.LogWarning("Недостатньо прав : {error} ", ex.Message);
                context.Response.StatusCode = 403;
                await context.Response.WriteAsJsonAsync(ApiResponse<object>.Error(ex.Message));
            }
            catch (BadRequestException ex)
            {
                _logger.LogInformation("Поганий запит : {error} ", ex.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(ApiResponse<object>.Error(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ну вот як так, непонятна помилка : {error} ", ex.Message);
                if (webHostEnvironment.IsDevelopment())
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync($"Внутрішня помилка сервера: {ex.Message}");
                    return;
                }
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(ApiResponse<object>.Error("Внутрішня помилка сервера"));
            }
        }
    }
}
