using Tiktok_Clone.DAL.Entities.Identity;
using Tiktok_Clone.DAL.Entities.Video;

namespace Tiktok_Clone.DAL.Entities.Like
{
    public class LikeEntity : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
        public required Guid UserId { get; set; }
        public Guid VideoId { get; set; }

        public UserEntity? User { get; set; }

        public VideoEntity? Video { get; set; }


    }
}
