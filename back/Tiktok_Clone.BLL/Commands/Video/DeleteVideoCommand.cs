using MediatR;

namespace Tiktok_Clone.BLL.Commands.Video
{
    public record DeleteVideoCommand(Guid VideoId, string UserId) : IRequest<Unit>;

}
