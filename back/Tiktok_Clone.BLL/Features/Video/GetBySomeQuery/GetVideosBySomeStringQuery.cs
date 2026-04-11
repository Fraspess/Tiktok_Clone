using MediatR;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Features.Video.GetBySomeQuery
{
    public record GetVideosBySomeStringQuery(string SomeString, PaginationSettings Settings) : IRequest<PagedResult<SimpleVideoDTO>>;

}
