using MediatR;

namespace Tiktok_Clone.BLL.Commands.Like
{
    public record ToogleLikeCommand(Guid VideoId, Guid UserId) : IRequest<Unit>;
}
