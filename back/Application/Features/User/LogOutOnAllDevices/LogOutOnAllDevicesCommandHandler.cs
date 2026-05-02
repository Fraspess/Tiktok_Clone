using Application.Interfaces;
using MediatR;

namespace Application.Features.User.LogOutOnAllDevices
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