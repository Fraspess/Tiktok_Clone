using MediatR;
using Tiktok_Clone.BLL.Features.Video.Create;

namespace Tiktok_Clone.BLL.Features.Video.Upload
{
    public record CreateVideoCommand(CreateVideoDTO Dto, Guid OwnerId) : IRequest<Unit>;
}
