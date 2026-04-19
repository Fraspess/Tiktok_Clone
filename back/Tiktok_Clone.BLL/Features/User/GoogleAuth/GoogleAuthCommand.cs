using MediatR;
using Tiktok_Clone.BLL.Dtos.Token;

namespace Tiktok_Clone.BLL.Features.User.GoogleAuth
{
    public record GoogleAuthCommand(string IdToken) : IRequest<TokenResponseDTO>;
}
