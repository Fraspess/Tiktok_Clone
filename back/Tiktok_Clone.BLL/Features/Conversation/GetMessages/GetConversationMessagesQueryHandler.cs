using MediatR;
using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.BLL.Pagination;
using Tiktok_Clone.BLL.Services.Conversation;

namespace Tiktok_Clone.BLL.Features.Conversation.GetMessages
{
    public class GetConversationMessagesQueryHandler(IConversationService service) : IRequestHandler<GetConversationMessagesQuery, PagedResult<MessageDTO>>
    {
        public async Task<PagedResult<MessageDTO>> Handle(GetConversationMessagesQuery request, CancellationToken cancellationToken)
        {
            return await service.GetConversationMessagesAsync(request.ConversationId, request.Settings, request.UserId);
        }
    }
}
