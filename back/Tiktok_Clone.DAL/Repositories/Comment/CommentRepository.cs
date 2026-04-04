using Tiktok_Clone.DAL.Entities.Comment;

namespace Tiktok_Clone.DAL.Repositories.Comment
{
    public class CommentRepository : GenericRepository<CommentEntity, Guid>, ICommentRepository
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
