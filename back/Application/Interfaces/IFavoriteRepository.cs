using Application.Interfaces;
using Domain.Entities.Favorite;

namespace Application.Interfaces.Favorite
{
    public interface IFavoriteRepository : IGenericRepository<FavoriteEntity, Guid>
    {
        public FavoriteEntity GetByVideoAndUserIds(Guid videoId, Guid userId);
    }
}
