namespace Tiktok_Clone.BLL.Services.Like
{
    public interface ILikeService
    {
        public Task ToogleLike(Guid videoId, Guid userId);
    }
}
