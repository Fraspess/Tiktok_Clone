using MediatR;
using Tiktok_Clone.BLL.Dtos.Conversation;
using Tiktok_Clone.BLL.Services.Conversation;

namespace Tiktok_Clone.BLL.Features.Conversation.Create
{
    public class CreateConversationCommandHandler(IConversationService service) : IRequestHandler<CreateConversationCommand, ConversationDTO>
    {
        public async Task<ConversationDTO> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
        {
            var conversation = await service.CreateConversationAsync(request.CurrentUserId, request.UsersIds);
            return conversation;
        }
    }
}
