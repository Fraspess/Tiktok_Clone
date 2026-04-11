using MediatR;
using Tiktok_Clone.BLL.Dtos.Token;
using Tiktok_Clone.BLL.Services.Token;

namespace Tiktok_Clone.BLL.Features.User.RefreshTokens
{
    public class RefreshTokensCommandHandler(IJWTTokenService jWTTokenService)
        : IRequestHandler<RefreshTokensCommand, TokenResponseDTO>
    {
        public async Task<TokenResponseDTO> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
        {
            return await jWTTokenService.RefreshTokensAsync(request.refreshToken);
        }
    }
}
