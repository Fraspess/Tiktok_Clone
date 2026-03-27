using MediatR;
using Tiktok_Clone.BLL.Commands.User;
using Tiktok_Clone.BLL.Dtos.Token;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.BLL.Services.User;

namespace Tiktok_Clone.BLL.Handlers.User
{
    public class RegisterUserCommandHandler(IUserService userService) : IRequestHandler<RegisterUserCommand, TokenResponseDTO>
    {

        async Task<TokenResponseDTO> IRequestHandler<RegisterUserCommand, TokenResponseDTO>.Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var tokens = await userService.Register(new RegisterUserDTO
            {
                Email = request.email,
                Password = request.password,
                Username = request.username,
                Avatar = request.avatar
            });
            return tokens;
        }
    }
}
