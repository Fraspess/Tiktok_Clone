using MediatR;

namespace Tiktok_Clone.BLL.Features.Favorite.ToggleFavorite
{
    public record ToggleFavoriteCommand(Guid VideoId, Guid UserId) : IRequest<Unit>;

}
