using MediatR;
using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.BLL.Pagination;
using Tiktok_Clone.BLL.Queries.Video;
using Tiktok_Clone.BLL.Services.Video;

namespace Tiktok_Clone.BLL.Handlers.Video
{
    public class GetForYouPageVideosQueryHandler(IVideoService videoService) : IRequestHandler<GetForYouPageVideosQuery, PagedResult<VideoDTO>>
    {
        public async Task<PagedResult<VideoDTO>> Handle(GetForYouPageVideosQuery request, CancellationToken cancellationToken)
        {
            return await videoService.GetForYouPageVideos(request.PaginationSettings);
        }
    }
}
