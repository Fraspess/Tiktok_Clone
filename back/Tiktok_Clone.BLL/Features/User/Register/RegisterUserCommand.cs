using MediatR;
using Microsoft.AspNetCore.Http;

namespace Tiktok_Clone.BLL.Features.User.Register
{
    public record RegisterUserCommand(string Username, string Email, string Password, IFormFile? Avatar) : IRequest<Unit>;

}
