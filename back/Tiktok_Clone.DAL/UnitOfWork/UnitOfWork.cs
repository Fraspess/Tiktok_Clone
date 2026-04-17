using Tiktok_Clone.DAL.Repositories.Comment;
using Tiktok_Clone.DAL.Repositories.Conversation;
using Tiktok_Clone.DAL.Repositories.Favorite;
using Tiktok_Clone.DAL.Repositories.Follow;
using Tiktok_Clone.DAL.Repositories.HashTag;
using Tiktok_Clone.DAL.Repositories.HashTags;
using Tiktok_Clone.DAL.Repositories.Like;
using Tiktok_Clone.DAL.Repositories.Video;

namespace Tiktok_Clone.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ICommentRepository Comments { get; }
        public IVideoRepository Videos { get; }
        public ILikeRepository Likes { get; }
        public IHashTagRepository HashTags { get; }
        public IFollowRepository Follows { get; }
        public IFavoriteRepository Favorites { get; }
        public IConversationRepository Conversations { get; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Comments = new CommentRepository(context);
            Videos = new VideoRepository(context);
            Likes = new LikeRepository(context);
            HashTags = new HashTagRepository(context);
            Follows = new FollowRepository(context);
            Favorites = new FavoriteRepository(context);
            Conversations = new ConversationRepository(context);
        }

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
