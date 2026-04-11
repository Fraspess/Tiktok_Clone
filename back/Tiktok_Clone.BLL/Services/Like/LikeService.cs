using Tiktok_Clone.BLL.Exceptions;
using Tiktok_Clone.DAL.Entities.Like;
using Tiktok_Clone.DAL.UnitOfWork;

namespace Tiktok_Clone.BLL.Services.Like
{
    public class LikeService(IUnitOfWork _uow) : ILikeService
    {
        // Якщо є лайк забирає, нема - ставить
        public async Task ToogleLike(Guid videoId, Guid userId)
        {
            var video = await _uow.Videos.GetByIdAsync(videoId)
                ?? throw new NotFoundException("Відео не знайдено");

            var existingLike = await _uow.Likes.GetLikeByUserAndVideoIdAsync(userId, videoId);

            if (existingLike == null)
            {
                await _uow.Likes.CreateAsync(new LikeEntity
                {
                    UserId = userId,
                    VideoId = videoId
                });
            }
            else
            {
                await _uow.Likes.DeleteAsync(existingLike);
            }

            await _uow.SaveChangesAsync();
        }
    }
}
