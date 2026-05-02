using Application.Interfaces;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Follow
{
    internal class FollowRepository : IFollowRepository
    {
        private AppDbContext _context;

        public FollowRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserFollowEntity?> GetFollowAsync(Guid who, Guid whom)
        {
            return await _context.UserFollows
                .FirstOrDefaultAsync(f => f.FollowerId == who && f.FollowingId == whom);
        }


        public async Task<bool> IsFollowingAsync(Guid who, Guid whom)
        {
            return await _context.UserFollows.AnyAsync(f => f.FollowerId == who && f.FollowingId == whom);
        }

        public async Task<int> GetFollowersCountAsync(Guid userId)
        {
            return await _context.UserFollows.CountAsync(f => f.FollowingId == userId);
        }

        public async Task<int> GetFollowingCountAsync(Guid userId)
        {
            return await _context.UserFollows.CountAsync(f => f.FollowerId == userId);
        }
    }
}