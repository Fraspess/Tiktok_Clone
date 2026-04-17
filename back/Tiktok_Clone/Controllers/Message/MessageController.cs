using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.BLL.Features.Message.Get;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.Controllers.Message
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController(IMediator _mediator) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMessages(Guid conversationId, int pageNumber = 1, int pageSize = 10)
        {
            var messages = await _mediator.Send(new GetMessagesQuery(conversationId, new PaginationSettings { PageNumber = pageNumber, PageSize = pageSize }));
            return Ok(ApiResponse<PagedResult<MessageDTO>>.Success(messages));
        }

    }
}
