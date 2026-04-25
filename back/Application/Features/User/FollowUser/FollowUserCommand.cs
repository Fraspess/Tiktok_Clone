using MediatR;

namespace Application.Features.User.FollowUser
{
    public record FollowUserCommand(Guid FollowerId, Guid FollowingId) : IRequest<Unit>;
}
