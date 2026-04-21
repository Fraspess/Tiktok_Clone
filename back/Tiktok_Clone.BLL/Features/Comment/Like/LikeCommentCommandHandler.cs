using MediatR;
using Tiktok_Clone.BLL.Services.Comment;

namespace Tiktok_Clone.BLL.Features.Comment.Like
{
    public class LikeCommentCommandHandler(ICommentService service) : IRequestHandler<LikeCommentCommand, Unit>
    {
        async Task<Unit> IRequestHandler<LikeCommentCommand, Unit>.Handle(LikeCommentCommand request, CancellationToken cancellationToken)
        {
            await service.ToggleLikeAsync(request.CommentId, request.UserId);
            return Unit.Value;
        }
    }
}
