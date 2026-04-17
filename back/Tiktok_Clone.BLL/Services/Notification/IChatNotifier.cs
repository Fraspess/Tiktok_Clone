using Tiktok_Clone.BLL.Dtos.Message;

namespace Tiktok_Clone.BLL.Services.Notification
{
    public interface IChatNotifier
    {
        Task SendMessageAsync(Guid recipientId, MessageDTO message);
        Task SendPendingMessagesAsync(Guid recipientId, IEnumerable<MessageDTO> messages);
    }
}
