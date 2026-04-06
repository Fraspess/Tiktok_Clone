using MediatR;
using Tiktok_Clone.BLL.Commands.Comment;
using Tiktok_Clone.BLL.Services.Comment;

namespace Tiktok_Clone.BLL.Handlers.Comment
{
    public class DeleteCommentCommandHandler(ICommentService service) : IRequestHandler<DeleteCommentCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            await service.DeleteCommentAsync(request.CommentId, request.UserId);
            return Unit.Value;
        }
    }
}
