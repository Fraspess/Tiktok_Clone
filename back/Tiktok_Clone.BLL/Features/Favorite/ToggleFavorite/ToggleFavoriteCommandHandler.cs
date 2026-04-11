using MediatR;
using Tiktok_Clone.BLL.Services.Favorite;

namespace Tiktok_Clone.BLL.Features.Favorite.ToggleFavorite
{
    public class ToggleFavoriteCommandHandler(IFavoriteService service) : IRequestHandler<ToggleFavoriteCommand, Unit>
    {
        public async Task<Unit> Handle(ToggleFavoriteCommand request, CancellationToken cancellationToken)
        {
            await service.ToggleFavoriteAsync(request.VideoId, request.UserId);
            return Unit.Value;
        }
    }
}
