using Microsoft.AspNetCore.Http;

namespace Tiktok_Clone.BLL.Services.ImageService
{
    public interface IImageService
    {
        public Task<String> SaveImageAsync(IFormFile imageFile);
        public Task<String> SaveImageAsync(String url);
        public void DeleteImage(String imageName);
    }
}
