using MediatR;
using Tiktok_Clone.BLL.Dtos.Token;

namespace Tiktok_Clone.BLL.Commands.User
{
    public record RefreshTokensCommand(string refreshToken) : IRequest<TokenResponseDTO>;
}
