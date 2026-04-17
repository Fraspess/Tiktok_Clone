using MediatR;
using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Features.DM.Get
{
    public record GetInboxMessagesCommand(Guid userId, PaginationSettings settings) : IRequest<PagedResult<MessageDTO>>;
}
