using Tiktok_Clone.DAL.Entities.Conversation;
using Tiktok_Clone.DAL.Entities.Identity;

namespace Tiktok_Clone.DAL.Entities.Message
{
    public class MessageEntity : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();

        public required Guid SenderId { get; set; }
        public UserEntity Sender { get; set; } = null!;
        public bool IsDelivered { get; set; } = false;

        public required String Content { get; set; }

        public bool IsRead { get; set; } = false;

        public Guid ConversationId { get; set; }

        public ConversationEntity Conversation { get; set; } = null!;
    }
}
