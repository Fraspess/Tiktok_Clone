using MediatR;
using Tiktok_Clone.BLL.Commands.User;
using Tiktok_Clone.BLL.Services.User;

namespace Tiktok_Clone.BLL.Handlers.User
{
    public class ForgotPasswordCommandHandler(IUserService userService) : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            await userService.ForgotPasswordAsync(request.email);
            return Unit.Value;
        }
    }
}
