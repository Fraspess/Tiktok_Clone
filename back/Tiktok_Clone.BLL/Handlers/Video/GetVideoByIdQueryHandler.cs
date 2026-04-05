using MediatR;
using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.BLL.Queries.Video;
using Tiktok_Clone.BLL.Services.Video;

namespace Tiktok_Clone.BLL.Handlers.Video
{
    public class GetVideoByIdQueryHandler(IVideoService videoService) : IRequestHandler<GetVideoByIdQuery, VideoDTO>
    {
        public async Task<VideoDTO> Handle(GetVideoByIdQuery request, CancellationToken cancellationToken)
        {
            return await videoService.GetVideoByIdAsync(request.Id, request.UserId);
        }
    }
}
