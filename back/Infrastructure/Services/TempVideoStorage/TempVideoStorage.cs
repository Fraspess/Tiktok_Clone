using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.TempStorage
{
    internal class TempVideoStorage : ITempVideoStorage
    {
        String tempPath = Path.Combine(Directory.GetCurrentDirectory(), "videos");
        public async Task<string> SaveVideoAsync(IFormFile file)
        {
            Directory.CreateDirectory(tempPath);

            var inputPath = Path.Combine(tempPath, "Input");
            Directory.CreateDirectory(inputPath);

            var filePath = Path.Combine(inputPath, $"{Guid.NewGuid().ToString()}.mp4");
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return stream.Name;
        }
    }
}
