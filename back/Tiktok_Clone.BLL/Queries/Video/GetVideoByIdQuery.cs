using MediatR;
using Tiktok_Clone.BLL.Dtos.Video;

namespace Tiktok_Clone.BLL.Queries.Video
{
    public record GetVideoByIdQuery(Guid id) : IRequest<VideoDTO>;
}
