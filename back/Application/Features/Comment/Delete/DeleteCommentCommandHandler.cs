using Application.Interfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Comment.Delete
{
    public class DeleteCommentCommandHandler(IUnitOfWork _uow) : IRequestHandler<DeleteCommentCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _uow.Comments.GetByIdAsync(request.CommentId)
                ?? throw new ValidationException("Коментарій не знайдено");
            if (comment.UserId == request.UserId)
            {
                await _uow.Comments.DeleteAsync(comment);
            }
            else
            {
                throw new NotAllowedException("Ви не маєте прав на це");
            }
            await _uow.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
