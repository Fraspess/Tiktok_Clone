using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Services.Video
{
    public interface IVideoService
    {
        Task<VideoDTO> UploadVideoAsync(CreateVideoDTO dto, Guid ownerId);

        Task<VideoDTO> GetVideoByIdAsync(Guid id);

        Task DeleteVideoById(Guid id, Guid userId);

        Task<PagedResult<VideoDTO>> GetForYouPageVideos(PaginationSettings paginationSettings);
    }
}
