using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Tiktok_Clone.BLL.Dtos.Video;
using Tiktok_Clone.BLL.Exceptions;
using Tiktok_Clone.BLL.Extensions;
using Tiktok_Clone.BLL.Pagination;
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
    public class VideoService(IVideoRepository _videoRepository, IHashTagRepository _hashTagRepository, IMapper _mapper, UserManager<UserEntity> _userManager) : IVideoService
    {
        public async Task DeleteVideoById(Guid id, Guid userId)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId)
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
            return await _videoRepository
                .GetAll()
                .ProjectTo<VideoDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(v => v.Id == id) ?? throw new NotFoundException("Відео не знайдено");

        }

        public async Task<PagedResult<VideoDTO>> GetForYouPageVideos(PaginationSettings paginationSettings)
        {
            var videos = await _videoRepository
                .GetAll()
                .OrderBy(v => Guid.NewGuid())
                .ProjectTo<VideoDTO>(_mapper.ConfigurationProvider)
                .ToPagedResultAsync(paginationSettings);

            return videos;
        }

        public async Task<VideoDTO> UploadVideoAsync(CreateVideoDTO dto, Guid ownerId)
        {
            var fileName = await SaveVideoFileAsync(dto.VideoFile);
            // витягуємо хештеги
            var parsedDescription = ParseDescription(dto.Description);


            var newVideo = new VideoEntity()
            {
                UserId = ownerId,
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

        public async Task UploadVideoAsyncDev(string url, string key, Guid[] randomUsersId, string videoDescription = "Good description salo")
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Videos");
            Directory.CreateDirectory(uploadFolder);
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", key);
                var response = await httpClient.GetStringAsync(url);

                var json = JsonConvert.DeserializeObject<dynamic>(response);

                foreach (var video in json!.videos)
                {
                    string videoUrl = null!;
                    foreach (var file in video.video_files)
                    {
                        if (file.quality == "hd")
                        {
                            videoUrl = file.link;
                            break;
                        }
                    }
                    if (videoUrl == null) continue;

                    var fileName = $"{Guid.NewGuid()}.mp4";
                    var savePath = Path.Combine(uploadFolder, fileName);

                    var bytes = await httpClient.GetByteArrayAsync(videoUrl);
                    await File.WriteAllBytesAsync(savePath, bytes); ;

                    var randomUserId = randomUsersId[Random.Shared.Next(randomUsersId.Count())];

                    var newVideo = new VideoEntity { Description = videoDescription, UserId = randomUserId, VideoFileName = fileName };
                    await _videoRepository.CreateAsync(newVideo);
                }
            }


        }
    }
}
