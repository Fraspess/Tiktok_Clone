namespace Tiktok_Clone.BLL.Services.Favorite
{
    public interface IFavoriteService
    {
        public Task ToggleFavoriteAsync(Guid videoId, Guid userId);
    }
}
