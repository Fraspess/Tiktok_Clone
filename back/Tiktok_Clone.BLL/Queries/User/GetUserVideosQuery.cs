using MediatR;
using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Queries.User
{
    public record GetUserVideosQuery(Guid UserId, PaginationSettings Settings, Guid? CurrentUserId) : IRequest<PagedResult<VideoDTO>>;
}
