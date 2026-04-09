using MediatR;
using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.BLL.Pagination;
using Tiktok_Clone.BLL.Queries.User;
using Tiktok_Clone.BLL.Services.Video;

namespace Tiktok_Clone.BLL.Handlers.User
{
    public class GetUserVideosQueryHandler(IVideoService service) : IRequestHandler<GetUserVideosQuery, PagedResult<VideoDTO>>
    {
        public async Task<PagedResult<VideoDTO>> Handle(GetUserVideosQuery request, CancellationToken cancellationToken)
        {
            return await service.GetUserVideos(request.UserId, request.Settings, request.CurrentUserId);
        }
    }
}
