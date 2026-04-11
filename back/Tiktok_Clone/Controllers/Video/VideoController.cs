using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.BLL.Extensions;
using Tiktok_Clone.BLL.Features.Video.Create;
using Tiktok_Clone.BLL.Features.Video.Delete;
using Tiktok_Clone.BLL.Features.Video.GetById;
using Tiktok_Clone.BLL.Features.Video.GetBySomeQuery;
using Tiktok_Clone.BLL.Features.Video.GetFYP;
using Tiktok_Clone.BLL.Features.Video.Upload;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.Controllers.Video
{
    [Route("api/videos")]
    [ApiController]
    public class VideoController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("video/{fileName}")]
        public IActionResult GetVideoFileByFileName(string fileName)
        {
            var videoFile = Path.Combine(Directory.GetCurrentDirectory(), "Videos", fileName);
            if (!System.IO.File.Exists(videoFile))
            {
                return NotFound(ApiResponse<string>.Error("Відео не знайдено"));
            }
            var stream = System.IO.File.OpenRead(videoFile);
            return File(stream, "video/mp4", enableRangeProcessing: true);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideoById(Guid id)
        {
            var video = await _mediator.Send(new GetVideoByIdQuery(id, GetUserIfExists()));
            if (video is null)
            {
                return NotFound(ApiResponse<string>.Error("Відео не знайдено"));
            }
            return Ok(ApiResponse<VideoDTO>.Success(video));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(Guid id)
        {
            var userId = User.GetUserId();
            await _mediator.Send(new DeleteVideoCommand(id, userId));
            return Ok(ApiResponse<string>.Success("Відео успішно видалено"));
        }

        [Authorize]
        [RequestSizeLimit(287_000_000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 287_000_000)]
        [HttpPost]
        public async Task<IActionResult> UploadVideo([FromForm] CreateVideoDTO dto)
        {
            var userId = User.GetUserId();
            await _mediator.Send(new CreateVideoCommand(dto, userId));
            return Ok(ApiResponse<string>.Success("Відео успішно завантажено"));
        }



        [HttpGet("fyp")]
        public async Task<IActionResult> GetForYouPage([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {

            var videos = await _mediator.Send(new GetForYouPageVideosQuery(new PaginationSettings { PageNumber = pageNumber, PageSize = pageSize }, GetUserIfExists()));
            return Ok(ApiResponse<PagedResult<VideoDTO>>.Success(videos));
        }

        [HttpGet("search/{query}")]
        public async Task<IActionResult> GetVideoBySomeQuery(string query, int pageNumber = 1, int pageSize = 5)
        {
            var videos = await _mediator.Send(new GetVideosBySomeStringQuery(query, new PaginationSettings { PageNumber = pageNumber, PageSize = pageSize }));
            return Ok(ApiResponse<PagedResult<SimpleVideoDTO>>.Success(videos));
        }

        private Guid? GetUserIfExists()
        {
            Guid? currentUserId = null;

            if (User.Identity?.IsAuthenticated == true)
                currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return currentUserId;
        }
    }
}
