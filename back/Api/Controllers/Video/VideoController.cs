using Application;
using Application.Dtos.Video;
using Application.Extensions;
using Application.Features.Video.Delete;
using Application.Features.Video.GetById;
using Application.Features.Video.GetBySomeQuery;
using Application.Features.Video.GetFYP;
using Application.Features.Video.Upload;
using Application.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers.Video
{
    [Route("api/videos")]
    [ApiController]
    public class VideoController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("video/{fileName}")]
        public IActionResult GetVideoFileByFileName(string fileName)
        {
            var videoFile = Path.Combine(Directory.GetCurrentDirectory(), "videos", "output", fileName);
            if (!System.IO.File.Exists(videoFile))
            {
                return NotFound(ApiResponse<string>.Error("Відео не знайдено"));
            }
            var stream = System.IO.File.OpenRead(videoFile);
            return File(stream, "video/mp4", enableRangeProcessing: true, fileDownloadName: "video.mp4");
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
        [RequestSizeLimit(500_000_000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 500_000_000)]
        [HttpPost]
        public async Task<IActionResult> UploadVideo([FromForm] CreateVideoDTO dto)
        {
            var userId = User.GetUserId();
            await _mediator.Send(new UploadVideoCommand(dto, userId));
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
