using Application.Interfaces;
using Domain.Entities.Like;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Like;

internal class LikeRepository : GenericRepository<LikeEntity, Guid>, ILikeRepository
{
    public LikeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<LikeEntity?> GetLikeByUserAndVideoIdAsync(Guid userId, Guid videoId)
    {
        return await _context.Likes.FirstOrDefaultAsync(l => l.UserId == userId && l.VideoId == videoId);
    }
}