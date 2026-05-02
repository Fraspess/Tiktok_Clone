using Domain.Entities.Identity;
using Domain.Entities.Video;

namespace Domain.Entities.Favorite
{
    public class FavoriteEntity : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
        public Guid VideoId { get; set; }
        public VideoEntity Video { get; set; } = null!;
        public Guid UserId { get; set; }
        public UserEntity User { get; set; } = null!;
    }
}