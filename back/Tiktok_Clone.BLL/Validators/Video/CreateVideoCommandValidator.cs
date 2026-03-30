using FluentValidation;
using Microsoft.AspNetCore.Http;
using Tiktok_Clone.BLL.Commands.Video;
using Xabe.FFmpeg;

namespace Tiktok_Clone.BLL.Validators.Video
{
    public class CreateVideoCommandValidator : AbstractValidator<CreateVideoCommand>
    {
        public CreateVideoCommandValidator()
        {
            RuleFor(x => x.Dto.Description)
                .NotNull().WithMessage("Опис не може бути порожнім")
                .NotEmpty().WithMessage("Опис відео не може бути порожнім");

            RuleFor(x => x.Dto.VideoFile)
                .NotNull().WithMessage("Відео файл не може бути порожнім")
                .Must(file => file.Length > 0).WithMessage("Відео файл не може бути порожнім")
                .MustAsync(IsValidVideo).WithMessage("Формат відео не підтримується");
        }




        private async Task<bool> IsValidVideo(IFormFile file, CancellationToken ct)
        {
            var tempPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}");

            using (var stream = File.Create(tempPath))
                await file.CopyToAsync(stream);

            try
            {
                var mediaInfo = await FFmpeg.GetMediaInfo(tempPath);

                if (mediaInfo.Duration.TotalSeconds < 1)
                    return false;

                return mediaInfo.VideoStreams.Any();

            }
            catch
            {
                return false;
            }
            finally
            {
                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
        }
    }
}
