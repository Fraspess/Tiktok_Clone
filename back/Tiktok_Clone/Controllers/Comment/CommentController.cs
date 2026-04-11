using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Dtos.Comment;
using Tiktok_Clone.BLL.Extensions;
using Tiktok_Clone.BLL.Features.Comment.Create;
using Tiktok_Clone.BLL.Features.Comment.Delete;
using Tiktok_Clone.BLL.Features.Comment.Get;
using Tiktok_Clone.BLL.Features.Comment.GetReplies;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.Controllers.Comment
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDTO dto)
        {
            await _mediator.Send(new CreateCommentCommand(dto, User.GetUserId()));
            return Ok(ApiResponse<object>.Success(null!, "Успішно створено коментар"));
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(Guid videoId, int pageNumber = 1, int pageSize = 20)
        {
            var comments = await _mediator.Send(new GetCommentsQuery(videoId, new PaginationSettings { PageNumber = pageNumber, PageSize = pageSize }));
            return Ok(ApiResponse<PagedResult<CommentDTO>>.Success(comments, null));
        }

        [HttpGet("replies")]
        public async Task<IActionResult> GetReplies(Guid commentId, int pageNumber = 1, int pageSize = 5)
        {
            var replies = await _mediator.Send(new GetRepliesQuery(commentId, new PaginationSettings { PageNumber = pageNumber, PageSize = pageSize }));
            return Ok(ApiResponse<PagedResult<CommentDTO>>.Success(replies, null));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            await _mediator.Send(new DeleteCommentCommand(commentId, User.GetUserId()));
            return Ok(ApiResponse<object>.Success(null!, "Успішно видалено коментар"));
        }
    }
}
