using Microsoft.AspNetCore.SignalR;
using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.BLL.Services.Notification;
using Tiktok_Clone.Hubs;

namespace Tiktok_Clone.Notifications
{
    public class ChatNotifier(IHubContext<ChatHub> hubContext) : IChatNotifier
    {
        public async Task SendMessageAsync(Guid recipientId, MessageDTO message)
        {
            await hubContext.Clients
                .User(recipientId.ToString())
                .SendAsync("ReceivedMessage", message);
        }

        public async Task SendPendingMessagesAsync(Guid recipientId, IEnumerable<MessageDTO> messages)
        {
            await hubContext.Clients
                .User(recipientId.ToString())
                .SendAsync("ReceivedPendingMessages", messages);
        }
    }
}
