using Tiktok_Clone.DAL.Entities.Message;

namespace Tiktok_Clone.DAL.Repositories.Message
{
    public class MessageRepository : GenericRepository<MessageEntity, Guid>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
