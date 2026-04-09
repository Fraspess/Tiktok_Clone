using Tiktok_Clone.DAL.Entities.Favorite;

namespace Tiktok_Clone.DAL.Repositories.Favorite
{
    public class FavoriteRepository : GenericRepository<FavoriteEntity, Guid>, IFavoriteRepository
    {
        public FavoriteRepository(AppDbContext context) : base(context)
        {
        }

        public FavoriteEntity GetByVideoAndUserIds(Guid videoId, Guid userId)
        {
            return _context.Favorites.Where(f => f.VideoId == videoId && f.UserId == userId).FirstOrDefault()!;
        }

    }
}
