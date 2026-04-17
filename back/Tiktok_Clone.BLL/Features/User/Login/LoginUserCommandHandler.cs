using MediatR;
using Tiktok_Clone.BLL.Dtos.Token;
using Tiktok_Clone.BLL.Services.User;

namespace Tiktok_Clone.BLL.Features.User.Login
{
    public class LoginUserCommandHandler(IUserService userService)
        : IRequestHandler<LoginUserCommand, TokenResponseDTO>
    {
        public async Task<TokenResponseDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await userService.Login(new LoginUserDTO
            {
                Login = request.login,
                Password = request.password
            });
        }
    }
}
