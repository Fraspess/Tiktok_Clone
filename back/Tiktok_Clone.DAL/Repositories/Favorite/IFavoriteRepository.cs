using Tiktok_Clone.DAL.Entities.Favorite;

namespace Tiktok_Clone.DAL.Repositories.Favorite
{
    public interface IFavoriteRepository : IGenericRepository<FavoriteEntity, Guid>
    {
        public FavoriteEntity GetByVideoAndUserIds(Guid videoId, Guid userId);
    }
}
