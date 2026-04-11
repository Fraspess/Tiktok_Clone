using MediatR;
using Tiktok_Clone.BLL.Dtos.Token;

namespace Tiktok_Clone.BLL.Features.User.ConfirmEmail
{
    public record ConfirmEmailCommand(string Email, string Token) : IRequest<TokenResponseDTO>;

}
