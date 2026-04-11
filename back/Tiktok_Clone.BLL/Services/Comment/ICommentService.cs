using Tiktok_Clone.BLL.Dtos.Comment;
using Tiktok_Clone.BLL.Features.Comment.Create;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Services.Comment
{
    public interface ICommentService
    {
        Task CreateCommentAsync(CreateCommentDTO dto, Guid ownerId);

        Task<PagedResult<CommentDTO>> GetCommentsAsync(Guid videoId, PaginationSettings settings);

        Task<PagedResult<CommentDTO>> GetRepliesAsync(Guid parentCommentId, PaginationSettings settings);

        Task DeleteCommentAsync(Guid commentId, Guid userId);
    }
}
