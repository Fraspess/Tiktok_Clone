using Tiktok_Clone.DAL.Entities.Identity;
using Tiktok_Clone.DAL.Entities.Video;

namespace Tiktok_Clone.DAL.Entities.Favorite
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
