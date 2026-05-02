using Domain.Entities.HashTags;

namespace Application.Interfaces
{
    public interface IHashTagRepository : IGenericRepository<HashTagEntity, Guid>
    {
        public Task<HashTagEntity?> GetByNameAsync(string name);
    }
}