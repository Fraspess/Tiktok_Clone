using MediatR;
using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Queries.Video
{
    public record GetForYouPageVideosQuery(PaginationSettings PaginationSettings) : IRequest<PagedResult<VideoDTO>>;

}
