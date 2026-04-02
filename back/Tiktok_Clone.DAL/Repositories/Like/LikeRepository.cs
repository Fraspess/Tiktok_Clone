using Tiktok_Clone.DAL.Entities.Like;

namespace Tiktok_Clone.DAL.Repositories.Like;

public class LikeRepository : GenericRepository<LikeEntity, Guid>, ILikeRepository
{
    public LikeRepository(AppDbContext context) : base(context) { }

    public LikeEntity GetLikeByUserAndVideoId(Guid userId, Guid videoId)
    {
        return _context.Likes.FirstOrDefault(l => l.UserId == userId && l.VideoId == videoId)!;
    }


}

