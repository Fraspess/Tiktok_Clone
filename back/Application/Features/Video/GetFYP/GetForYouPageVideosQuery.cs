using Application.Dtos.Video;
using Application.Pagination;
using MediatR;

namespace Application.Features.Video.GetFYP
{
    public record GetForYouPageVideosQuery(PaginationSettings PaginationSettings, Guid? UserId) : IRequest<PagedResult<VideoDTO>>;

}
