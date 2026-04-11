using MediatR;
using Tiktok_Clone.BLL.Dtos.Comment;
using Tiktok_Clone.BLL.Features.Comment.Get;
using Tiktok_Clone.BLL.Pagination;
using Tiktok_Clone.BLL.Services.Comment;

namespace Tiktok_Clone.BLL.Features.Comment.GetReplies
{
    public class GetCommentsQueryHandler(ICommentService service) : IRequestHandler<GetCommentsQuery, PagedResult<CommentDTO>>
    {
        public async Task<PagedResult<CommentDTO>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            return await service.GetCommentsAsync(request.VideoId, request.PaginationSettings);
        }
    }
}
