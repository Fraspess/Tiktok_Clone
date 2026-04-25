using MediatR;

namespace Application.Features.Comment.Delete
{
    public record DeleteCommentCommand(Guid CommentId, Guid UserId) : IRequest<Unit>;
}
