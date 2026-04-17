using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Services.Message
{
    public interface IMessageService
    {
        public Task SendAsync(Guid userId, Guid conversationId, string content);
        public Task FlushPendingAsync(Guid userId);
        public Task<PagedResult<MessageDTO>> GetMessagesAsync(Guid conversationId, PaginationSettings settings);
        public Task MarkAsReadAsync(Guid userId, Guid messageId);
        public Task MarkAsDeliveredAsync(Guid userId, Guid messageId);

    }
}
