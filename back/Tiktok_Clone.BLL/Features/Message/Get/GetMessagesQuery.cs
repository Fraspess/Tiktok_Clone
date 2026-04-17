using MediatR;
using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Features.Message.Get
{
    public record GetMessagesQuery(Guid ConversationId, PaginationSettings Settings) : IRequest<PagedResult<MessageDTO>>;
}
