using Tiktok_Clone.DAL.Entities.Video;

namespace Tiktok_Clone.DAL.Repositories.Video
{
    public class VideoRepository : GenericRepository<VideoEntity, Guid>, IVideoRepository
    {
        public VideoRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}
