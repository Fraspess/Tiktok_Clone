using Application.Interfaces.Favorite;
using Domain.Entities.Favorite;

namespace Persistence.Repositories.Favorite
{
    internal class FavoriteRepository : GenericRepository<FavoriteEntity, Guid>, IFavoriteRepository
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
