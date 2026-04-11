using MediatR;

namespace Tiktok_Clone.BLL.Features.Comment.Delete
{
    public record DeleteCommentCommand(Guid CommentId, Guid UserId) : IRequest<Unit>;
}
