using Tiktok_Clone.DAL.Entities.Identity;

namespace Tiktok_Clone.DAL.Entities.Comment
{
    public class CommentLikeEntity
    {
        public Guid CommentId { get; set; }

        public Guid UserId { get; set; }

        public UserEntity? User { get; set; }

        public CommentEntity? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
