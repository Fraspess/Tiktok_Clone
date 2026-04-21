using MediatR;
using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Features.Conversation.GetMessages
{
    public record GetConversationMessagesQuery(Guid ConversationId, PaginationSettings Settings, Guid UserId) : IRequest<PagedResult<MessageDTO>>;
}
