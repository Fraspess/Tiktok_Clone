using Tiktok_Clone.DAL.Entities.Like;

namespace Tiktok_Clone.DAL.Repositories.Like
{
    public interface ILikeRepository : IGenericRepository<LikeEntity, Guid>
    {
        public Task<LikeEntity?> GetLikeByUserAndVideoIdAsync(Guid userId, Guid videoId);
    }
}
