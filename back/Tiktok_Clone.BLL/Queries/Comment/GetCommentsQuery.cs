using MediatR;
using Tiktok_Clone.BLL.Dtos.Comment;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Queries.Comment
{
    public record GetCommentsQuery(Guid VideoId, PaginationSettings PaginationSettings) : IRequest<PagedResult<CommentDTO>>;
}
