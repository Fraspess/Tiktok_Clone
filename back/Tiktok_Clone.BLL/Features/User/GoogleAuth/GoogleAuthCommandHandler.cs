using MediatR;
using Tiktok_Clone.BLL.Dtos.Token;
using Tiktok_Clone.BLL.Services.User;

namespace Tiktok_Clone.BLL.Features.User.GoogleAuth
{
    public class GoogleAuthCommandHandler(IUserService service) : IRequestHandler<GoogleAuthCommand, TokenResponseDTO>
    {
        public async Task<TokenResponseDTO> Handle(GoogleAuthCommand request, CancellationToken cancellationToken)
        {
            return await service.GoogleAuth(request.IdToken);
        }
    }
}
