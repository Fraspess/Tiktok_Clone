using AutoMapper;
using Tiktok_Clone.DAL.Entities.Message;
using Tiktok_Clone.DAL.UnitOfWork;

namespace Tiktok_Clone.BLL.Services.Message
{
    public class MessageService(IUnitOfWork _uow, IMapper _mapper) : IMessageService
    {
        public Task FlushPendingAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task SendAsync(Guid userId, Guid conversationId, string content)
        {

            var newMessage = new MessageEntity
            {
                SenderId = userId,
                ConversationId = conversationId,
                Text = content
            };

        }
    }
}
