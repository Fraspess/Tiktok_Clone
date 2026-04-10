using Microsoft.EntityFrameworkCore;
using Tiktok_Clone.DAL.Entities.Video;

namespace Tiktok_Clone.DAL.Repositories.Video
{
    public class VideoRepository : GenericRepository<VideoEntity, Guid>, IVideoRepository
    {
        public VideoRepository(AppDbContext context)
            : base(context)
        {

        }

        public async Task<VideoEntity?> FindVideoBySomeString(string someString)
        {
            return await _context.Videos
                .Include(v => v.HashTags)
                .Include(v => v.Author)
                .FirstOrDefaultAsync(v =>
                    v.HashTags.Any(h => h.HashTag.Tag.StartsWith(someString)) || v.Author!.UserName == someString);
        }
    }
}
