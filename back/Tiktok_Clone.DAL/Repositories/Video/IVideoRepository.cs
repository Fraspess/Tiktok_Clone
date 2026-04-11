using Tiktok_Clone.DAL.Entities.Video;

namespace Tiktok_Clone.DAL.Repositories.Video
{
    public interface IVideoRepository : IGenericRepository<VideoEntity, Guid>
    {
        public Task<VideoEntity?> FindVideoBySomeString(string someString);
    }
}
