using Tiktok_Clone.DAL.Entities.Message;

namespace Tiktok_Clone.DAL.Entities.Conversation
{
    public class ConversationEntity : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();

        public ICollection<ConversationParticipant> Participants { get; set; } = [];
        public ICollection<MessageEntity> Messages { get; set; } = [];
    }
}
