using Tiktok_Clone.DAL.Repositories.Comment;
using Tiktok_Clone.DAL.Repositories.Favorite;
using Tiktok_Clone.DAL.Repositories.Follow;
using Tiktok_Clone.DAL.Repositories.HashTags;
using Tiktok_Clone.DAL.Repositories.Like;
using Tiktok_Clone.DAL.Repositories.Video;

namespace Tiktok_Clone.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICommentRepository Comments { get; }
        IVideoRepository Videos { get; }
        ILikeRepository Likes { get; }
        IHashTagRepository HashTags { get; }
        IFollowRepository Follows { get; }
        IFavoriteRepository Favorites { get; }

        Task<int> SaveChangesAsync();
    }
}
