using MediatR;

namespace Application.Features.User.LogOutOnAllDevices
{
    public record LogOutOnAllDevicesCommand(Guid UserId) : IRequest<Unit>;
}