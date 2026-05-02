using Application.Interfaces;
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