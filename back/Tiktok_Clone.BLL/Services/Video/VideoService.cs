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
using Tiktok_Clone.DAL.UnitOfWork;
using Xabe.FFmpeg;

namespace Tiktok_Clone.BLL.Services.Video
{
    // клас для того щоб парсить хештеги з опису відео
    public class ParsedDescription
    {
        public string CleanText { get; set; } = String.Empty;
        public List<string> Tags { get; set; } = new List<string>();
    }
    public class VideoService(IMapper _mapper, UserManager<UserEntity> _userManager, IUnitOfWork _uow) : IVideoService
    {
        public async Task DeleteVideoById(Guid id, Guid userId)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId)
                ?? throw new UnauthorizedException("Користувача не знайдено. Невалідний токен");

            var video = await _uow.Videos.GetByIdAsync(id)
                ?? throw new NotFoundException("Відео не знайдено");

            if (video.Author != user)
            {
                throw new NotAllowedException("Ви не маєте прав на цю дію");
            }

            await _uow.Videos.DeleteAsync(video);
        }

        public async Task<VideoDTO> GetVideoByIdAsync(Guid id, Guid? userId)
        {
            return await _uow.Videos
                .GetAll()
                .ProjectTo<VideoDTO>(_mapper.ConfigurationProvider, new { currentUserId = userId })
                .FirstOrDefaultAsync(v => v.Id == id) ?? throw new NotFoundException("Відео не знайдено");

        }

        public async Task<PagedResult<VideoDTO>> GetForYouPageVideos(PaginationSettings paginationSettings, Guid? userId)
        {
            var videos = await _uow.Videos
                .GetAll()
                .OrderBy(v => Guid.NewGuid())
                .ProjectTo<VideoDTO>(_mapper.ConfigurationProvider, new { currentUserId = userId })
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

            var hashtags = await GetOrCreateHashtagsAsync(parsedDescription.Tags);
            foreach (var tag in hashtags)
                newVideo.HashTags.Add(new VideoHashTagEntity { HashTagId = tag.Id, VideoId = newVideo.Id });

            await _uow.Videos.CreateAsync(newVideo);
            await _uow.SaveChangesAsync();
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

        public async Task UploadVideoAsyncDev(string url, string key, Guid[] randomUsersId, string videoDescription = "Good description salo #salo #potuzhno #ukraine #football #sport")
        {
            Console.WriteLine($"Raw description: '{videoDescription}'");
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

                    var parsedDescription = ParseDescription(videoDescription);
                    var newVideo = new VideoEntity { Description = parsedDescription.CleanText, UserId = randomUserId, VideoFileName = fileName };

                    var hashtags = await GetOrCreateHashtagsAsync(parsedDescription.Tags);
                    foreach (var tag in hashtags)
                    {
                        newVideo.HashTags.Add(new VideoHashTagEntity { HashTagId = tag.Id, VideoId = newVideo.Id });
                    }
                    await _uow.Videos.CreateAsync(newVideo);
                }
            }
            await _uow.SaveChangesAsync();


        }

        public async Task<PagedResult<VideoDTO>> GetUserVideos(Guid userId, PaginationSettings settings, Guid? currentUser)
        {
            var videos = await _uow.Videos
                .GetAll()
                .Where(v => v.UserId == userId)
                .OrderBy(v => v.CreatedAt)
                .ProjectTo<VideoDTO>(_mapper.ConfigurationProvider, new { currentUserId = currentUser })
                .ToPagedResultAsync(settings);
            return videos;
        }

        public async Task<PagedResult<SimpleVideoDTO>> FindVideosBySomeStringAsync(string someString, PaginationSettings settings)
        {
            someString = someString.ToLower().Trim();
            var query = _uow.Videos
                .GetAll()
                .Include(v => v.HashTags)
                .Include(v => v.Author)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(someString))
            {
                query = query.Where(v =>
                    v.Description.ToLower().Contains(someString) ||
                    v.Author!.UserName!.ToLower().Contains(someString) ||
                    v.HashTags.Any(h => h.HashTag.Tag.ToLower().Contains(someString))
                );
            }

            var videos = await query
                .OrderByDescending(v => v.CreatedAt)
                .ProjectTo<SimpleVideoDTO>(_mapper.ConfigurationProvider)
                .ToPagedResultAsync(settings);

            return videos;

        }

        private async Task<List<HashTagEntity>> GetOrCreateHashtagsAsync(List<string> tags)
        {
            var result = new List<HashTagEntity>();
            foreach (var tagName in tags)
            {
                var tag = await _uow.HashTags.GetByNameAsync(tagName)
                       ?? _uow.HashTags.GetTracked(h => h.Tag == tagName);

                if (tag is null)
                {
                    tag = new HashTagEntity { Tag = tagName };
                    await _uow.HashTags.CreateAsync(tag);
                }
                result.Add(tag);
            }
            return result;
        }
    }
}
