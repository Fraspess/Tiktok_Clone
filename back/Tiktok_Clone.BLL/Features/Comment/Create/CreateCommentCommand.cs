using MediatR;

namespace Tiktok_Clone.BLL.Features.Comment.Create
{
    public record CreateCommentCommand(CreateCommentDTO Dto, Guid OwnerId) : IRequest<Unit>;
}
