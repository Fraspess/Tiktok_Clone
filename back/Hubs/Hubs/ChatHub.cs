using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Tiktok_Clone.BLL.Services.Message;

namespace Tiktok_Clone.Hubs
{
    [Authorize]
    public class ChatHub(IMessageService messageService) : Hub
    {
        public async Task SendMessage(Guid conversationId, string content)
        {
            await messageService.SendAsync(Guid.Parse(Context.UserIdentifier!), conversationId, content);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier!;
            await messageService.FlushPendingAsync(Guid.Parse(userId));
            await base.OnConnectedAsync();
        }

        public async Task MarkAsDelivered(Guid messageId)
        {
            await messageService.MarkAsDeliveredAsync(Guid.Parse(Context.UserIdentifier!), messageId);
        }
        public async Task MarkAsRead(Guid messageId)
        {
            await messageService.MarkAsReadAsync(Guid.Parse(Context.UserIdentifier!), messageId);
        }
    }
}
