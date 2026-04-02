using Microsoft.AspNetCore.Identity;
using Tiktok_Clone.BLL.Exceptions;
using Tiktok_Clone.DAL.Entities.Identity;
using Tiktok_Clone.DAL.Entities.Like;
using Tiktok_Clone.DAL.Repositories.Like;
using Tiktok_Clone.DAL.Repositories.Video;

namespace Tiktok_Clone.BLL.Services.Like
{
    public class LikeService(ILikeRepository _likeRepository, IVideoRepository _videoRepository, UserManager<UserEntity> _userManager) : ILikeService
    {
        // Якщо є лайк забирає, нема - ставить
        public async Task ToogleLike(Guid videoId, Guid userId)
        {
            var video = await _videoRepository.GetByIdAsync(videoId) ?? throw new NotFoundException("Відео не знайдено");
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId) ?? throw new NotFoundException("Користувач не знайдений");

            var newLikeEntity = _likeRepository.GetLikeByUserAndVideoId(userId, videoId);
            if (newLikeEntity == null)
            {
                newLikeEntity = new LikeEntity
                {
                    UserId = userId,
                    VideoId = videoId
                };
                await _likeRepository.CreateAsync(newLikeEntity);
                user.Likes.Add(newLikeEntity);
                video.Likes.Add(newLikeEntity);
            }
            else
            {
                user.Likes.Remove(newLikeEntity);
                video.Likes.Remove(newLikeEntity);
            }
            await _userManager.UpdateAsync(user);
        }
    }
}
