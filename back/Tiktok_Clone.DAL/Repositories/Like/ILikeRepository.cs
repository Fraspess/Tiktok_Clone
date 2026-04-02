using Tiktok_Clone.DAL.Entities.Like;

namespace Tiktok_Clone.DAL.Repositories.Like
{
    public interface ILikeRepository : IGenericRepository<LikeEntity, Guid>
    {
        public LikeEntity GetLikeByUserAndVideoId(Guid userId, Guid videoId);
    }
}
