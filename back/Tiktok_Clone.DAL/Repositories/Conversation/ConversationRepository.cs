using Tiktok_Clone.DAL.Entities.Conversation;

namespace Tiktok_Clone.DAL.Repositories.Conversation
{
    public class ConversationRepository : GenericRepository<ConversationEntity, Guid>, IConversationRepository
    {
        public ConversationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
