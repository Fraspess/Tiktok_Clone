using Tiktok_Clone.BLL.Exceptions;
using Tiktok_Clone.BLL.Services.Video;
using Tiktok_Clone.DAL.Entities.Favorite;
using Tiktok_Clone.DAL.Repositories.Favorite;

namespace Tiktok_Clone.BLL.Services.Favorite
{
    public class FavoriteService(IFavoriteRepository repository, IVideoService videoService) : IFavoriteService
    {
        public async Task ToogleFavoriteAsync(Guid videoId, Guid userId)
        {
            var video = await videoService.GetVideoByIdAsync(videoId, userId)
                ?? throw new NotFoundException("Відео не знайдено");

            var favoriteEntity = repository.GetByVideoAndUserIds(videoId, userId);
            if (favoriteEntity is null)
            {
                favoriteEntity = new FavoriteEntity
                {
                    UserId = userId,
                    VideoId = videoId,
                };
                await repository.CreateAsync(favoriteEntity);
            }
            else
            {
                await repository.DeleteAsync(favoriteEntity);
            }
        }
    }
}
