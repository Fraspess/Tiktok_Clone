using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using Tiktok_Clone.BLL.Services.ImageService;

namespace Tiktok_Clone.BLL.Services.Images
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<ImageService> _logger;
        private readonly HttpClient _httpClient = new HttpClient();

        public ImageService(IWebHostEnvironment environment, ILogger<ImageService> logger)
        {
            _environment = environment;
            _logger = logger;
        }


        public void DeleteImage(string imageName)
        {
            var imageFolder = Path.Combine(_environment.ContentRootPath, "Images");
            var path = Path.Combine(imageFolder, imageName);

            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while deleting image. Error : {error} ", ex.Message);
                return;
            }
        }

        private async Task<String> SaveImagePrivate(Stream stream)
        {
            try
            {
                var imageFolder = Path.Combine(_environment.ContentRootPath, "Images");
                if (!Directory.Exists(imageFolder))
                {
                    Directory.CreateDirectory(imageFolder);
                }
                using Image image = Image.Load(stream);
                var imageName = Guid.NewGuid().ToString() + ".webp";
                await image.SaveAsWebpAsync(Path.Combine(imageFolder, imageName));
                return imageName;

            }
            catch (Exception ex)
            {
                _logger.LogError("Error while saving image. Error : {error} ", ex.Message);
                return String.Empty;
            }
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var stream = imageFile.OpenReadStream();
            return await SaveImagePrivate(stream);

        }


        public async Task<string> SaveImageAsync(string url)
        {
            var httpStream = await _httpClient.GetStreamAsync(url);
            var stream = new MemoryStream();

            await httpStream.CopyToAsync(stream);
            stream.Position = 0;
            return await SaveImagePrivate(stream);
        }
    }
}

