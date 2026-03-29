using MediatR;
using Microsoft.AspNetCore.Http;

namespace Tiktok_Clone.BLL.Commands.User
{
    public record RegisterUserCommand(string Username, string Email, string Password, IFormFile? Avatar) : IRequest<Unit>;

}
