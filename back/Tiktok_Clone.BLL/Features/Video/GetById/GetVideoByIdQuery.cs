using MediatR;
using Tiktok_Clone.BLL.Dtos.Video;

namespace Tiktok_Clone.BLL.Features.Video.GetById
{
    public record GetVideoByIdQuery(Guid Id, Guid? UserId) : IRequest<VideoDTO>;
}
