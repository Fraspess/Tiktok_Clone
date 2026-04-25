using Application.Interfaces;
using Domain.Entities.Comment;

namespace Application.Interfaces.Comment
{
    public interface ICommentRepository : IGenericRepository<CommentEntity, Guid>
    {
        public IQueryable<CommentEntity> GetCommentsByVideoId(Guid videoId);
        public IQueryable<CommentEntity> GetRepliesAsync(Guid commentId);
    }
}
