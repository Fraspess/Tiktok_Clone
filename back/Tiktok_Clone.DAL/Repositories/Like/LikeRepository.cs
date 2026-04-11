using Microsoft.EntityFrameworkCore;
using Tiktok_Clone.DAL.Entities.Like;

namespace Tiktok_Clone.DAL.Repositories.Like;

public class LikeRepository : GenericRepository<LikeEntity, Guid>, ILikeRepository
{
    public LikeRepository(AppDbContext context) : base(context) { }

    public async Task<LikeEntity?> GetLikeByUserAndVideoIdAsync(Guid userId, Guid videoId)
    {
        return await _context.Likes.FirstOrDefaultAsync(l => l.UserId == userId && l.VideoId == videoId);
    }


}

