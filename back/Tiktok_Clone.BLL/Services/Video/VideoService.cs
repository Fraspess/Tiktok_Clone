using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.BLL.Exceptions;
using Tiktok_Clone.DAL.Entities.HashTags;
using Tiktok_Clone.DAL.Entities.Identity;
using Tiktok_Clone.DAL.Entities.Video;
using Tiktok_Clone.DAL.Repositories.HashTags;
using Tiktok_Clone.DAL.Repositories.Video;
using Xabe.FFmpeg;

namespace Tiktok_Clone.BLL.Services.Video
{
    // клас для того щоб парсить хештеги з опису відео
    public class ParsedDescription
    {
        public string CleanText { get; set; } = String.Empty;
        public List<string> Tags { get; set; } = new List<string>();
    }
    public class VideoService(ILogger<VideoService> _logger, IVideoRepository _videoRepository, IHashTagRepository _hashTagRepository, IMapper _mapper, UserManager<UserEntity> _userManager) : IVideoService
    {
        public async Task DeleteVideoById(Guid id, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new UnauthorizedException("Користувача не знайдено. Невалідний токен");

            var video = await _videoRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Відео не знайдено");

            if (video.Author != user)
            {
                throw new NotAllowedException("Ви не маєте прав на цю дію");
            }

            await _videoRepository.DeleteAsync(video);
        }

        public async Task<VideoDTO> GetVideoByIdAsync(Guid id)
        {
            return _mapper.Map<VideoDTO>(await _videoRepository.GetByIdAsync(id));
        }

        public async Task<VideoDTO> UploadVideoAsync(CreateVideoDTO dto, string ownerId)
        {
            var fileName = await SaveVideoFileAsync(dto.VideoFile);
            // витягуємо хештеги
            var parsedDescription = ParseDescription(dto.Description);


            var newVideo = new VideoEntity()
            {
                UserId = Guid.Parse(ownerId),
                Description = parsedDescription.CleanText,
                VideoFileName = fileName
            };

            // зберігаємо відео в базі даних щоб потім звязати з хештегами
            await _videoRepository.CreateAsync(newVideo);

            var hashTags = parsedDescription.Tags;
            foreach (var hashTag in hashTags)
            {
                var tag = await _hashTagRepository.GetByNameAsync(hashTag);
                if (tag is null)
                {
                    tag = new HashTagEntity() { Tag = hashTag };
                    await _hashTagRepository.CreateAsync(tag);
                }

                newVideo.HashTags.Add(new VideoHashTagEntity
                {
                    HashTagId = tag.Id,
                    VideoId = newVideo.Id
                });
            }

            await _videoRepository.UpdateAsync(newVideo);
            return _mapper.Map<VideoDTO>(newVideo);
        }

        private ParsedDescription ParseDescription(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new ParsedDescription { CleanText = "", Tags = new List<string>() };

            var tags = Regex.Matches(input, @"#(\w+)")
                            .Select(m => m.Groups[1].Value.ToLower())
                            .Distinct()
                            .ToList();

            var cleanText = Regex.Replace(input, @"#\w+", "").Trim();
            cleanText = Regex.Replace(cleanText, @"\s{2,}", " ");

            return new ParsedDescription { CleanText = cleanText, Tags = tags };
        }


        private async Task<string> SaveVideoFileAsync(IFormFile file)
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Videos");
            Directory.CreateDirectory(uploadFolder);

            var fileName = Guid.NewGuid().ToString();
            var inputPath = Path.Combine(uploadFolder, file.FileName);
            var outputPath = Path.Combine(uploadFolder, fileName + ".mp4");
            // зберігаємо исходник
            await using (var stream = File.Create(inputPath))
                await file.CopyToAsync(stream);
            try
            {
                // отримуємо інформацію про відео(медіа) кине exception якщо не media
                var mediaInfo = await FFmpeg.GetMediaInfo(inputPath);

                var videoStream = mediaInfo.VideoStreams.FirstOrDefault()?.SetCodec(VideoCodec.h264);
                var audioStream = mediaInfo.AudioStreams.FirstOrDefault()?.SetCodec(AudioCodec.aac);

                // конвертуємо відео в mp4
                var conversion = FFmpeg.Conversions.New().SetOutput(outputPath);
                conversion.AddStream(videoStream);
                // аудіо потік може бути null 
                if (audioStream != null) conversion.AddStream(audioStream);

                await conversion.Start();

                // удаляємо исходник
                File.Delete(inputPath);
                // вертаємо назву файла і розширення щоб зберегти в БД
                return fileName + ".mp4";

            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (File.Exists(inputPath))
                    File.Delete(inputPath);
            }
        }

    }
}
