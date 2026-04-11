using MediatR;
using Tiktok_Clone.BLL.Dtos.Token;
using Tiktok_Clone.BLL.Services.User;

namespace Tiktok_Clone.BLL.Features.User.ConfirmEmail
{
    public class ConfirmEmailCommandHandler(IUserService userService) : IRequestHandler<ConfirmEmailCommand, TokenResponseDTO>
    {
        public async Task<TokenResponseDTO> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            return await userService.ConfirmEmail(request.Email, request.Token);
        }
    }

}
