using MediatR;

namespace Tiktok_Clone.BLL.Features.Comment.Like
{
    public record LikeCommentCommand(Guid CommentId, Guid UserId) : IRequest<Unit>;
}
