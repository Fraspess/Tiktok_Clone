using MediatR;
using Microsoft.AspNetCore.Http;
using Tiktok_Clone.BLL.Dtos.Token;

namespace Tiktok_Clone.BLL.Commands.User
{
    public record RegisterUserCommand(string username, string email, string password, IFormFile? avatar) : IRequest<TokenResponseDTO>;

}
