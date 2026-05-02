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
        IReportRepository Reports { get; }

        Task<int> SaveChangesAsync();
    }
}