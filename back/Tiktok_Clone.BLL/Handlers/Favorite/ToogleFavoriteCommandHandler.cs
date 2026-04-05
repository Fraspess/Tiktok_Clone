using MediatR;
using Tiktok_Clone.BLL.Commands.Favorite;
using Tiktok_Clone.BLL.Services.Favorite;

namespace Tiktok_Clone.BLL.Handlers.Favorite
{
    public class ToogleFavoriteCommandHandler(IFavoriteService service) : IRequestHandler<ToogleFavoriteCommand, Unit>
    {
        public async Task<Unit> Handle(ToogleFavoriteCommand request, CancellationToken cancellationToken)
        {
            await service.ToogleFavoriteAsync(request.VideoId, request.UserId);
            return Unit.Value;
        }
    }
}
