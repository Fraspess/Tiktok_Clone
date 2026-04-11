using Microsoft.AspNetCore.Http;

namespace Tiktok_Clone.BLL.Features.Video.Create
{
    public class CreateVideoDTO
    {
        public required IFormFile VideoFile { get; set; }

        public required string Description { get; set; }
    }
}
