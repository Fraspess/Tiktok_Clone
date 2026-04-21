using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Dtos.Conversation;
using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.BLL.Extensions;
using Tiktok_Clone.BLL.Features.Conversation.Create;
using Tiktok_Clone.BLL.Features.Conversation.Get;
using Tiktok_Clone.BLL.Features.Conversation.GetMessages;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.Controllers.Conversation
{
    [Route("api/conversations")]
    [ApiController]
    public class ConversationController(IMediator _mediator) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetConversations(int pageNumber = 1, int pageSize = 10)
        {
            var conversations = await _mediator.Send(new GetConversationsQuery(User.GetUserId(), new PaginationSettings { PageNumber = pageNumber, PageSize = pageSize }));
            return Ok(ApiResponse<PagedResult<ConversationDTO>>.Success(conversations));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateConverastion([FromBody] CreateConversationDTO dto)
        {
            var conversation = await _mediator.Send(new CreateConversationCommand(dto.UserIds, User.GetUserId()));
            return Ok(ApiResponse<ConversationDTO>.Success(conversation));
        }

        [HttpGet("messages")]
        [Authorize]
        public async Task<IActionResult> GetMessages(Guid conversationId, int pageNumber = 1, int pageSize = 10)
        {
            var messages = await _mediator.Send(new GetConversationMessagesQuery(conversationId, new PaginationSettings { PageNumber = pageNumber, PageSize = pageSize }, User.GetUserId()));
            return Ok(ApiResponse<PagedResult<MessageDTO>>.Success(messages));
        }

    }
}
