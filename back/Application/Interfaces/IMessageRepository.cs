using Application.Interfaces;
using Domain.Entities.Message;

namespace Application.Interfaces.Message
{
    public interface IMessageRepository : IGenericRepository<MessageEntity, Guid>
    {
    }
}
