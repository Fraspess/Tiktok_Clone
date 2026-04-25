using MediatR;

namespace Application.Features.Comment.Like
{
    public record LikeCommentCommand(Guid CommentId, Guid UserId) : IRequest<Unit>;
}
