using Application.Interfaces.Comment;
using Application.Interfaces.Conversation;
using Application.Interfaces.Favorite;
using Application.Interfaces.HashTags;
using Application.Interfaces.Like;
using Application.Interfaces.Message;
using Application.Interfaces.Video;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICommentRepository Comments { get; }
        IVideoRepository Videos { get; }
        ILikeRepository Likes { get; }
        IHashTagRepository HashTags { get; }
        IFollowRepository Follows { get; }
        IFavoriteRepository Favorites { get; }
        IConversationRepository Conversations { get; }
        IMessageRepository Messages { get; }

        Task<int> SaveChangesAsync();
    }
}
