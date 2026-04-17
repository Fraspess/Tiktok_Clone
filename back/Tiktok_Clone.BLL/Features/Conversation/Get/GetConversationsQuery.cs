using MediatR;
using Tiktok_Clone.BLL.Dtos.Conversation;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Features.Conversation.Get
{
    public record GetConversationsQuery(Guid UserId, PaginationSettings PaginationSettings) : IRequest<PagedResult<ConversationDTO>>;

}
