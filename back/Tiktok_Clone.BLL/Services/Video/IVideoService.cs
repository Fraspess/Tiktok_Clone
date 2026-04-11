using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.BLL.Features.Video.Create;
using Tiktok_Clone.BLL.Features.Video.GetBySomeQuery;
using Tiktok_Clone.BLL.Pagination;

namespace Tiktok_Clone.BLL.Services.Video
{
    public interface IVideoService
    {
        Task<VideoDTO> UploadVideoAsync(CreateVideoDTO dto, Guid ownerId);

        Task UploadVideoAsyncDev(string url, string key, Guid[] randomUsersId, string videoDescription = "Good description salo #salo #potuzhno #ukraine #football #sport");

        Task<VideoDTO> GetVideoByIdAsync(Guid id, Guid? userId);

        Task DeleteVideoById(Guid id, Guid userId);

        Task<PagedResult<VideoDTO>> GetForYouPageVideos(PaginationSettings paginationSettings, Guid? userId);

        Task<PagedResult<VideoDTO>> GetUserVideos(Guid userId, PaginationSettings settings, Guid? currentUserId);

        Task<PagedResult<SimpleVideoDTO>> FindVideosBySomeStringAsync(string someString, PaginationSettings settings);
    }
}
