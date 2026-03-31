using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Exceptions;

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
            catch (ValidationException ex)
            {
                _logger.LogWarning("Помилка валідації: {error} ", ex.Message);
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
                _logger.LogWarning("Не знайдено : {error} ", ex.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(ApiResponse<object>.Error(ex.Message));
            }
            catch (NotAllowedException ex)
            {
                _logger.LogWarning("Недостатньо прав : {error} ", ex.Message);
                context.Response.StatusCode = 403;
                await context.Response.WriteAsJsonAsync(ApiResponse<object>.Error(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError("Ну вот як так, непонятна помилка : {error} ", ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(ApiResponse<object>.Error("Внутрішня помилка сервера"));
            }
        }
    }
}
