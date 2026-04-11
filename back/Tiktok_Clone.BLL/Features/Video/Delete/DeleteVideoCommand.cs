using MediatR;

namespace Tiktok_Clone.BLL.Features.Video.Delete
{
    public record DeleteVideoCommand(Guid VideoId, Guid UserId) : IRequest<Unit>;

}
