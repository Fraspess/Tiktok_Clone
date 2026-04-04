using Tiktok_Clone.DAL.Entities.Comment;

namespace Tiktok_Clone.DAL.Repositories.Comment
{
    public interface ICommentRepository : IGenericRepository<CommentEntity, Guid>
    {
        public IQueryable<CommentEntity> GetCommentsByVideoId(Guid videoId);
        public IQueryable<CommentEntity> GetRepliesAsync(Guid commentId);
    }
}
