using Application.Interfaces;
using Application.Interfaces.Comment;
using Application.Interfaces.Conversation;
using Application.Interfaces.Favorite;
using Application.Interfaces.HashTags;
using Application.Interfaces.Like;
using Application.Interfaces.Message;
using Application.Interfaces.Video;
using Persistence.Repositories.Comment;
using Persistence.Repositories.Conversation;
using Persistence.Repositories.Favorite;
using Persistence.Repositories.Follow;
using Persistence.Repositories.HashTag;
using Persistence.Repositories.Like;
using Persistence.Repositories.Message;
using Persistence.Repositories.Video;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // Changed to concrete type to match the concrete implementation being instantiated.
        public ICommentRepository Comments { get; }
        public IVideoRepository Videos { get; }
        public ILikeRepository Likes { get; }
        public IHashTagRepository HashTags { get; }
        public IFollowRepository Follows { get; }
        public IFavoriteRepository Favorites { get; }
        public IConversationRepository Conversations { get; }
        public IMessageRepository Messages { get; }

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
            Messages = new MessageRepository(context);
        }

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
