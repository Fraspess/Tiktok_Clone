using Microsoft.AspNetCore.SignalR;
using Tiktok_Clone.BLL.Services.Message;

namespace Tiktok_Clone.Hubs
{
    public class ChatHub(IMessageService messageService) : Hub
    {
        public async Task SendMessage(Guid conversationId, string content)
        {

        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier!;
            await base.OnConnectedAsync();
        }
    }
}
