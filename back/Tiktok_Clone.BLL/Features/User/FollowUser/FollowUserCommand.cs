using MediatR;

namespace Tiktok_Clone.BLL.Features.User.FollowUser
{
    public record FollowUserCommand(Guid FollowerId, Guid FollowingId) : IRequest<Unit>;
}
