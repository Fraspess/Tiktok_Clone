using Tiktok_Clone.BLL.Dtos.Conversation;
using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Services.Conversation
{
    public interface IConversationService
    {
        public Task<ConversationDTO> GetConversationAsync(Guid conversationId, Guid userId);

        public Task<PagedResult<ConversationDTO>> GetConversationsAsync(Guid userId, PaginationSettings settings);

        public Task<ConversationDTO> CreateConversationAsync(Guid currentUserId, List<Guid> participants);

        public Task<PagedResult<MessageDTO>> GetConversationMessagesAsync(Guid conversationId, PaginationSettings settings, Guid userId);
    }
}
