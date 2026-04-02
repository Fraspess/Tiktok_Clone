using MediatR;
using Tiktok_Clone.BLL.Commands.Like;
using Tiktok_Clone.BLL.Services.Like;

namespace Tiktok_Clone.BLL.Handlers.Like
{
    public class ToogleLikeCommandHandler(ILikeService likeService) : IRequestHandler<ToogleLikeCommand, Unit>
    {
        public async Task<Unit> Handle(ToogleLikeCommand request, CancellationToken cancellationToken)
        {
            await likeService.ToogleLike(request.VideoId, request.UserId);
            return Unit.Value;
        }
    }
}
