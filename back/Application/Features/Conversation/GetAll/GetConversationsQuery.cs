using Application.Dtos.Conversation;
using Application.Pagination;
using MediatR;

namespace Application.Features.Conversation.GetAll
{
    public record GetConversationsQuery(Guid UserId, PaginationSettings PaginationSettings)
        : IRequest<PagedResult<ConversationDTO>>;
}