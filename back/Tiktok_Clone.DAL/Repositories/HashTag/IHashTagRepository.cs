using Tiktok_Clone.DAL.Entities.HashTags;

namespace Tiktok_Clone.DAL.Repositories.HashTags
{
    public interface IHashTagRepository : IGenericRepository<HashTagEntity, Guid>
    {
        public Task<HashTagEntity?> GetByNameAsync(string name);
    }
}
