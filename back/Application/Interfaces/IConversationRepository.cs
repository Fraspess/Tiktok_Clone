using Application.Interfaces;
using Domain.Entities.Conversation;

namespace Application.Interfaces.Conversation
{
    public interface IConversationRepository : IGenericRepository<ConversationEntity, Guid>
    {
    }
}
