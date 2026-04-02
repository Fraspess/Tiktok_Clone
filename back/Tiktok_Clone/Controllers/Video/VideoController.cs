using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tiktok_Clone.BLL;
using Tiktok_Clone.BLL.Commands.Video;
using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.BLL.Exceptions;
using Tiktok_Clone.BLL.Pagination;
using Tiktok_Clone.BLL.Queries.Video;

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
            var video = await _mediator.Send(new GetVideoByIdQuery(id));
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                 ?? throw new UnauthorizedException("Користувача не знайдено. Невалідний токен");
            await _mediator.Send(new DeleteVideoCommand(id, userId));
            return Ok(ApiResponse<string>.Success("Відео успішно видалено"));
        }

        [Authorize]
        [RequestSizeLimit(287_000_000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 287_000_000)]
        [HttpPost]
        public async Task<IActionResult> UploadVideo([FromForm] CreateVideoDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                 ?? throw new UnauthorizedException("Користувача не знайдено. Невалідний токен");
            await _mediator.Send(new CreateVideoCommand(dto, userId));
            return Ok(ApiResponse<string>.Success("Відео успішно завантажено"));
        }



        [HttpGet("fyp")]
        public async Task<IActionResult> GetForYouPage([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var videos = await _mediator.Send(new GetForYouPageVideosQuery(new PaginationSettings { PageNumber = pageNumber, PageSize = pageSize }));
            return Ok(ApiResponse<PagedResult<VideoDTO>>.Success(videos));
        }
    }
}
