using MediatR;
using Tiktok_Clone.BLL.Commands.User;
using Tiktok_Clone.BLL.Services.User;

namespace Tiktok_Clone.BLL.Handlers.User
{
    public class LogOutOnAllDevicesCommandHandler(IUserService userService)
        : IRequestHandler<LogOutOnAllDevicesCommand, Unit>
    {
        public async Task<Unit> Handle(LogOutOnAllDevicesCommand request, CancellationToken cancellationToken)
        {
            await userService.UpdateTokenVersion(request.UserId);
            return Unit.Value;
        }
    }
}
