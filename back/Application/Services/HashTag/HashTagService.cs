using Application.Interfaces;
using Domain.Entities.HashTags;

namespace Application.Services.HashTag
{
    internal class HashTagService(IUnitOfWork _uow) : IHashTagService
    {
        public async Task<List<HashTagEntity>> GetOrCreateAsync(List<string> tags)
        {
            var result = new List<HashTagEntity>();
            foreach (var tagName in tags)
            {
                var tag = await _uow.HashTags.GetByNameAsync(tagName)
                       ?? _uow.HashTags.GetTracked(h => h.Tag == tagName);

                if (tag is null)
                {
                    tag = new HashTagEntity { Tag = tagName };
                    await _uow.HashTags.CreateAsync(tag);
                }
                result.Add(tag);
            }
            return result;
        }
    }
}
