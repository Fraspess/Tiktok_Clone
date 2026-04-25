using Domain.Exceptions;
using System.Security.Claims;

namespace Application.Extensions
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
