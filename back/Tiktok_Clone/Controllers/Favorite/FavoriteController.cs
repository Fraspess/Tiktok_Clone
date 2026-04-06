using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Commands.Favorite;
using Tiktok_Clone.BLL.Extensions;


namespace Tiktok_Clone.Controllers.Favorite
{
    [Route("api/favorites")]
    [ApiController]
    public class FavoriteController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Favorite(Guid videoId)
        {
            await mediator.Send(new ToogleFavoriteCommand(videoId, User.GetUserId()));
            return Ok(ApiResponse<object>.Success(null!, null));
        }

    }
}
