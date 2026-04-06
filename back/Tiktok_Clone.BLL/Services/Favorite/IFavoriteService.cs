namespace Tiktok_Clone.BLL.Services.Favorite
{
    public interface IFavoriteService
    {
        public Task ToogleFavoriteAsync(Guid videoId, Guid userId);
    }
}
