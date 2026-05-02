using Application.Dtos.Comment;
using MediatR;

namespace Application.Features.Comment.Create
{
    public record CreateCommentCommand(CreateCommentDTO Dto, Guid OwnerId) : IRequest<Unit>;
}