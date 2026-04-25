using Application.Dtos.Conversation;
using MediatR;

namespace Application.Features.Conversation.Create
{
    public record CreateConversationCommand(List<Guid> UsersIds, Guid CurrentUserId) : IRequest<ConversationDTO>;
}
