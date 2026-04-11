using MediatR;
using Tiktok_Clone.BLL.Dtos.Token;

namespace Tiktok_Clone.BLL.Features.User.RefreshTokens
{
    public record RefreshTokensCommand(string refreshToken) : IRequest<TokenResponseDTO>;
}
