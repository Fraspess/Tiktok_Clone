using MediatR;
using Tiktok_Clone.BLL.Dtos.Comment;

namespace Tiktok_Clone.BLL.Commands.Comment
{
    public record CreateCommentCommand(CreateCommentDTO Dto, Guid OwnerId) : IRequest<Unit>;
}
