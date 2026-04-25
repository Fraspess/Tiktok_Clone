using Application.Interfaces.Conversation;
using Domain.Entities.Conversation;

namespace Persistence.Repositories.Conversation
{
    internal class ConversationRepository : GenericRepository<ConversationEntity, Guid>, IConversationRepository
    {
        public ConversationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
