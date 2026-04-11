using MediatR;
using Tiktok_Clone.BLL.Pagination;
using Tiktok_Clone.BLL.Services.Video;

namespace Tiktok_Clone.BLL.Features.Video.GetBySomeQuery
{
    public class GetVideosBySomeStringQueryHandler(IVideoService videoService) : IRequestHandler<GetVideosBySomeStringQuery, PagedResult<SimpleVideoDTO>>
    {
        public async Task<PagedResult<SimpleVideoDTO>> Handle(GetVideosBySomeStringQuery request, CancellationToken cancellationToken)
        {
            return await videoService.FindVideosBySomeStringAsync(request.SomeString, request.Settings);
        }
    }
}
