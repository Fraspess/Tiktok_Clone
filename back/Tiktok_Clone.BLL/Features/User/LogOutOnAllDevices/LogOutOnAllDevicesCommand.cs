using MediatR;

namespace Tiktok_Clone.BLL.Features.User.LogOutOnAllDevices
{
    public record LogOutOnAllDevicesCommand(Guid UserId) : IRequest<Unit>;
}
