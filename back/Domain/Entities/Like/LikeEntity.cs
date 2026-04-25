using Domain.Entities.Identity;
using Domain.Entities.Video;

namespace Domain.Entities.Like
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
