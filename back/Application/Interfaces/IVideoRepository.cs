using Domain.Entities.Video;

namespace Application.Interfaces.Video
{
    public interface IVideoRepository : IGenericRepository<VideoEntity, Guid>
    {
        public Task<VideoEntity?> FindVideoBySomeString(string someString);
    }
}
