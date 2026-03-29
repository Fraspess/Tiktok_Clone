using Microsoft.EntityFrameworkCore;
using Tiktok_Clone.DAL.Entities.HashTags;
using Tiktok_Clone.DAL.Repositories.HashTags;

namespace Tiktok_Clone.DAL.Repositories.HashTag
{
    public class HashTagRepository : GenericRepository<HashTagEntity, Guid>, IHashTagRepository
    {
        public HashTagRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<HashTagEntity?> GetByNameAsync(string name)
        {
            return await _context.HashTags.FirstOrDefaultAsync(h => h.Tag == name);
        }
    }
}
