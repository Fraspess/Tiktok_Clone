using Application.Interfaces.Message;
using Domain.Entities.Message;

namespace Persistence.Repositories.Message
{
    internal class MessageRepository : GenericRepository<MessageEntity, Guid>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
