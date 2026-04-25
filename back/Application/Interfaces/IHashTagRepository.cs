using Application.Interfaces;
using Domain.Entities.HashTags;

namespace Application.Interfaces.HashTags
{
    public interface IHashTagRepository : IGenericRepository<HashTagEntity, Guid>
    {
        public Task<HashTagEntity?> GetByNameAsync(string name);
    }
}
