using Domain.Entities.Identity;
using Domain.Entities.Video;

namespace Domain.Entities.Report
{
    public class ReportEntity : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();


        public required Guid SenderId { get; set; }
        public UserEntity Sender { get; set; } = null!;

        public Guid VideoId { get; set; }
        public VideoEntity? Video { get; set; }

        public Guid ReportedUserId { get; set; }
        public UserEntity? ReportedUser { get; set; }

        public required String Reason { get; set; }

    }
}
