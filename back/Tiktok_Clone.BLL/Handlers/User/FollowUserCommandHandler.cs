using MediatR;
using Tiktok_Clone.BLL.Commands.User;
using Tiktok_Clone.BLL.Services.User;

namespace Tiktok_Clone.BLL.Handlers.User
{
    public class FollowUserCommandHandler(IUserService service) : IRequestHandler<FollowUserCommand, Unit>
    {
        public async Task<Unit> Handle(FollowUserCommand request, CancellationToken cancellationToken)
        {
            await service.ToggleFollowAsync(request.FollowerId, request.FollowingId);
            return Unit.Value;
        }
    }
}
