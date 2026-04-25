using MediatR;

namespace Application.Features.LIke.ToogleLike
{
    public record ToogleLikeCommand(Guid VideoId, Guid UserId) : IRequest<Unit>;
}
