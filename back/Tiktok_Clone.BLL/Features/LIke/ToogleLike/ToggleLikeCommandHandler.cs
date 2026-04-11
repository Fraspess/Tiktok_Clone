using MediatR;
using Tiktok_Clone.BLL.Services.Like;

namespace Tiktok_Clone.BLL.Features.LIke.ToogleLike
{
    public class ToggleLikeCommandHandler(ILikeService likeService) : IRequestHandler<ToogleLikeCommand, Unit>
    {
        public async Task<Unit> Handle(ToogleLikeCommand request, CancellationToken cancellationToken)
        {
            await likeService.ToogleLike(request.VideoId, request.UserId);
            return Unit.Value;
        }
    }
}
