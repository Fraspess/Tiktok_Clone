using Domain.Entities.Video;

namespace Domain.Entities.HashTags
{
    public class HashTagEntity : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
        public required String Tag { get; set; }
        public ICollection<VideoHashTagEntity> VideoHashTags { get; set; } = new List<VideoHashTagEntity>();

    }
}
