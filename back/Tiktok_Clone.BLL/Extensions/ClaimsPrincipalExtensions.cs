using System.Security.Claims;
using Tiktok_Clone.BLL.Exceptions;

namespace Tiktok_Clone.BLL.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                 ?? throw new UnauthorizedException("Користувача не знайдено. Невалідний токен");
            return Guid.Parse(userId);
        }
    }
}
