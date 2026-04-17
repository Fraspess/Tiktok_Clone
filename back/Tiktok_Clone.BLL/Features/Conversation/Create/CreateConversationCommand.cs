using MediatR;
using Tiktok_Clone.BLL.Dtos.Conversation;

namespace Tiktok_Clone.BLL.Features.Conversation.Create
{
    public record CreateConversationCommand(List<Guid> UsersIds, Guid CurrentUserId) : IRequest<ConversationDTO>;
}
