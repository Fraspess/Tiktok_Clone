using MediatR;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.BLL.Services.User;

namespace Tiktok_Clone.BLL.Features.User.ResetPassword
{
    public class ResetPasswordCommandHandler(IUserService userService) : IRequestHandler<ResetPasswordCommand, Unit>
    {
        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            await userService.ResetPasswordAsync(new ResetPasswordDTO
            {
                Email = request.Email,
                NewPassword = request.NewPassword,
                Token = request.Token,
            });
            return Unit.Value;
        }
    }
}
