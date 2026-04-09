using MediatR;

namespace Tiktok_Clone.BLL.Commands.User
{
    public record FollowUserCommand(Guid FollowerId, Guid FollowingId) : IRequest<Unit>;
}
