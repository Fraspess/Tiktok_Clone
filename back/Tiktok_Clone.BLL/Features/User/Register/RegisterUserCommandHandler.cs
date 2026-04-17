using MediatR;
using Tiktok_Clone.BLL.Services.User;

namespace Tiktok_Clone.BLL.Features.User.Register
{
    public class RegisterUserCommandHandler(IUserService userService) : IRequestHandler<RegisterUserCommand, Unit>
    {

        async Task<Unit> IRequestHandler<RegisterUserCommand, Unit>.Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            await userService.Register(new RegisterUserDTO
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Avatar = request.Avatar
            });
            return Unit.Value;
        }
    }
}
