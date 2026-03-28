using MediatR;

namespace Tiktok_Clone.BLL.Commands.User
{
    public record LogOutOnAllDevicesCommand(string UserId) : IRequest<Unit>;
}
