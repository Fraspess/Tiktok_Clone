using Application.Dtos.Message;
using Application.Pagination;
using MediatR;

namespace Application.Features.Conversation.GetMessages
{
    public record GetConversationMessagesQuery(Guid ConversationId, PaginationSettings Settings, Guid UserId)
        : IRequest<PagedResult<MessageDTO>>;
}