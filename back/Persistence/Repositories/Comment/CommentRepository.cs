using Application.Interfaces.Comment;
using Domain.Entities.Comment;

namespace Persistence.Repositories.Comment
{
    internal class CommentRepository : GenericRepository<CommentEntity, Guid>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<CommentEntity> GetCommentsByVideoId(Guid videoId)
        {
            return _context.Comments.Where(c => c.VideoId == videoId && c.ParentCommentId == null);
        }

        public IQueryable<CommentEntity> GetRepliesAsync(Guid commentId)
        {
            return _context.Comments.Where(c => c.ParentCommentId == commentId);
        }
    }
}
