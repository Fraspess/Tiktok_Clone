using MediatR;

namespace Tiktok_Clone.BLL.Commands.Favorite
{
    public record ToogleFavoriteCommand(Guid VideoId, Guid UserId) : IRequest<Unit>;

}
