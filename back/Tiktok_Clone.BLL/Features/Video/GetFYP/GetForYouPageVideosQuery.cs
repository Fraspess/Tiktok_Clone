using MediatR;
using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Features.Video.GetFYP
{
    public record GetForYouPageVideosQuery(PaginationSettings PaginationSettings, Guid? UserId) : IRequest<PagedResult<VideoDTO>>;

}
