using MediatR;

namespace Tiktok_Clone.BLL.Commands.User
{
    public record LogOutOnAllDevicesCommand(Guid UserId) : IRequest<Unit>;
}
