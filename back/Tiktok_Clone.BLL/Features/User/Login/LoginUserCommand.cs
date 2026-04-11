using MediatR;
using Tiktok_Clone.BLL.Dtos.Token;

namespace Tiktok_Clone.BLL.Features.User.Login
{
    public record LoginUserCommand(string login, string password) : IRequest<TokenResponseDTO>;

}
