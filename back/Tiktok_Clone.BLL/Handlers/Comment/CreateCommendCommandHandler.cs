using MediatR;
using Tiktok_Clone.BLL.Commands.Comment;
using Tiktok_Clone.BLL.Services.Comment;

namespace Tiktok_Clone.BLL.Handlers.Comment
{
    public class CreateCommendCommandHandler(ICommentService service) : IRequestHandler<CreateCommentCommand, Unit>
    {
        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            await service.CreateCommentAsync(request.Dto, request.OwnerId);
            return Unit.Value;
        }
    }
}
