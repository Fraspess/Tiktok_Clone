using Application.Dtos.Message;

namespace Application.Interfaces
{
    public interface IChatNotifier
    {
        Task SendMessageAsync(Guid recipientId, MessageDTO message);
        Task SendPendingMessagesAsync(Guid recipientId, IEnumerable<MessageDTO> messages);
    }
}
