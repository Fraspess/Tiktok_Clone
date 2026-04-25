using Domain.Entities.Like;

namespace Application.Interfaces.Like
{
    public interface ILikeRepository : IGenericRepository<LikeEntity, Guid>
    {
        public Task<LikeEntity?> GetLikeByUserAndVideoIdAsync(Guid userId, Guid videoId);
    }
}
