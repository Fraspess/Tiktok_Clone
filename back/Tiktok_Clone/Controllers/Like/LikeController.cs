using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Extensions;
using Tiktok_Clone.BLL.Features.LIke.ToogleLike;

namespace Tiktok_Clone.Controllers.Like
{
    [Route("api/likes")]
    [ApiController]
    public class LikeController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ToogleLike(Guid videoId)
        {
            await _mediator.Send(new ToogleLikeCommand(videoId, User.GetUserId()));
            return Ok(ApiResponse<object>.Success(null!, null));
        }
    }
}
