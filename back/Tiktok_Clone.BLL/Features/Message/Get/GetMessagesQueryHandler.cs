using MediatR;
using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.BLL.Pagination;
using Tiktok_Clone.BLL.Services.Message;

namespace Tiktok_Clone.BLL.Features.Message.Get
{
    public class GetMessagesQueryHandler(IMessageService service) : IRequestHandler<GetMessagesQuery, PagedResult<MessageDTO>>
    {
        public async Task<PagedResult<MessageDTO>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            return await service.GetMessagesAsync(request.ConversationId, request.Settings);
        }
    }
}
