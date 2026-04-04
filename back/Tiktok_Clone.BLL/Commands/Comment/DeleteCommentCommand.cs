using MediatR;

namespace Tiktok_Clone.BLL.Commands.Comment
{
    public record DeleteCommentCommand(Guid CommentId, Guid UserId) : IRequest<Unit>;
}
