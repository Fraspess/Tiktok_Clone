using System.ComponentModel.DataAnnotations;
using Domain.Entities.Identity;

namespace Domain.Entities.Report
{
    public abstract class ReportEntity : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
        public Guid SenderId { get; set; }
        public UserEntity Sender { get; init; } = null!;
        public ReportReasons? Reason { get; set; }
        [MaxLength(255)]
        public String? OtherReason { get; set; }
    }
}