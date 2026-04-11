using Tiktok_Clone.BLL.Exceptions;
using Tiktok_Clone.BLL.Services.Video;
using Tiktok_Clone.DAL.Entities.Favorite;
using Tiktok_Clone.DAL.UnitOfWork;

namespace Tiktok_Clone.BLL.Services.Favorite
{
    public class FavoriteService(IVideoService _service, IUnitOfWork _uow) : IFavoriteService
    {
        public async Task ToggleFavoriteAsync(Guid videoId, Guid userId)
        {
            var video = await _service.GetVideoByIdAsync(videoId, userId)
                ?? throw new NotFoundException("Відео не знайдено");

            var favoriteEntity = _uow.Favorites.GetByVideoAndUserIds(videoId, userId);
            if (favoriteEntity is null)
            {
                favoriteEntity = new FavoriteEntity
                {
                    UserId = userId,
                    VideoId = videoId,
                };
                await _uow.Favorites.CreateAsync(favoriteEntity);
            }
            else
            {
                await _uow.Favorites.DeleteAsync(favoriteEntity);
            }
            await _uow.SaveChangesAsync();
        }
    }
}
