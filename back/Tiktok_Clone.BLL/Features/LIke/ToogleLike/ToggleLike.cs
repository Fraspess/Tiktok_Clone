using MediatR;

namespace Tiktok_Clone.BLL.Features.LIke.ToogleLike
{
    public record ToogleLikeCommand(Guid VideoId, Guid UserId) : IRequest<Unit>;
}
