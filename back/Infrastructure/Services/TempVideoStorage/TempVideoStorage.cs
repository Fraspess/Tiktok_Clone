using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.TempVideoStorage
{
    internal class TempVideoStorage : ITempVideoStorage
    {
        string tempPath = Path.Combine(Directory.GetCurrentDirectory(), "temp");

        public async Task<string> SaveVideoAsync(IFormFile file)
        {
            Directory.CreateDirectory(tempPath);
            
            Directory.CreateDirectory(tempPath);

            var filePath = Path.Combine(tempPath, $"{Guid.NewGuid().ToString()}.mp4");
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return stream.Name;
        }
    }
}