using MediatR;

namespace Application.Features.Favorite.ToggleFavorite
{
    public record ToggleFavoriteCommand(Guid VideoId, Guid UserId) : IRequest<Unit>;

}
