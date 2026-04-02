using MediatR;
using Tiktok_Clone.BLL.Dtos.Video;

namespace Tiktok_Clone.BLL.Commands.Video
{
    public record CreateVideoCommand(CreateVideoDTO Dto, Guid OwnerId) : IRequest<Unit>;
}
