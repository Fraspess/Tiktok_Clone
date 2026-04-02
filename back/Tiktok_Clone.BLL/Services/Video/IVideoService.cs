using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Services.Video
{
    public interface IVideoService
    {
        Task<VideoDTO> UploadVideoAsync(CreateVideoDTO dto, string ownerId);

        Task<VideoDTO> GetVideoByIdAsync(Guid id);

        Task DeleteVideoById(Guid id, string userId);

        Task<PagedResult<VideoDTO>> GetForYouPageVideos(PaginationSettings paginationSettings);
    }
}
