using Application.Interfaces;
using Domain.Entities.HashTags;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.HashTag
{
    internal class HashTagRepository : GenericRepository<HashTagEntity, Guid>, IHashTagRepository
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