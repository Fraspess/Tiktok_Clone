using MediatR;
using Tiktok_Clone.BLL.Dtos.Conversation;
using Tiktok_Clone.BLL.Pagination;
using Tiktok_Clone.BLL.Services.Conversation;

namespace Tiktok_Clone.BLL.Features.Conversation.Get
{
    public class GetConversationsQueryHandler(IConversationService service) : IRequestHandler<GetConversationsQuery, PagedResult<ConversationDTO>>
    {
        public async Task<PagedResult<ConversationDTO>> Handle(GetConversationsQuery request, CancellationToken cancellationToken)
        {
            return await service.GetConversationsAsync(request.UserId, request.PaginationSettings);
        }
    }
}
