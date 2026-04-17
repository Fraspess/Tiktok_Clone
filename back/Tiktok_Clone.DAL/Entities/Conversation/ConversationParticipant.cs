using Tiktok_Clone.DAL.Entities.Identity;

namespace Tiktok_Clone.DAL.Entities.Conversation
{
    public class ConversationParticipant : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }

        public DateTime? LastReadAt { get; set; }

        public Guid ConversationId { get; set; }
        public ConversationEntity Conversation { get; set; } = null!;
        public UserEntity User { get; set; } = null!;
    }
}
