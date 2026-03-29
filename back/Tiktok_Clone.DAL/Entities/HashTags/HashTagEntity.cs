using Tiktok_Clone.DAL.Entities.Video;

namespace Tiktok_Clone.DAL.Entities.HashTags
{
    public class HashTagEntity : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
        public required String Tag { get; set; }
        public ICollection<VideoHashTagEntity> VideoHashTags { get; set; } = new List<VideoHashTagEntity>();

    }
}
