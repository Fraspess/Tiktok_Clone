using Contracts.Events;
using FFMpegCore;
using MassTransit;

namespace VideoProcessor
{
    internal class VideoStartProcessingConsumer(ILogger<VideoStartProcessingConsumer> _logger, IConfiguration config, IPublishEndpoint publishEndpoint) : IConsumer<VideoStartProcessingEvent>
    {

        public async Task Consume(ConsumeContext<VideoStartProcessingEvent> context)
        {
            var inputPath = context.Message.FilePath;
            try
            {
                if (!File.Exists(inputPath))
                {
                    _logger.LogError("Input File {ErrorInput} wasnt found ", inputPath);
                    throw new Exception("File wasn't found");
                }

                await ValidateVideoAsync(inputPath);

                var outputPath = Path.GetFullPath(
                    Path.Combine(Path.GetDirectoryName(inputPath)!, "..", config["OutputVideoPath"]!, $"{Guid.NewGuid().ToString()}.mp4")
                );

                var outputDir = Path.GetDirectoryName(outputPath)!;
                Directory.CreateDirectory(outputDir);

                _logger.LogInformation("OUTPUT PATH: {OutputPath}", outputPath);
                _logger.LogInformation("Starting converting to mp4 at {Input}", inputPath);

                var videoInfo = await FFProbe.AnalyseAsync(inputPath);
                var duration = videoInfo.Duration;

                await FFMpegArguments
                    .FromFileInput(inputPath)
                    .OutputToFile(outputPath, overwrite: true, options => options
                        .WithVideoCodec("libx264")
                        .WithAudioCodec("aac")
                        .WithFastStart())
                    .NotifyOnProgress(async (progress) =>
                    {
                        var progressInPercents = (int)Math.Floor((progress / duration) * 100);
                        await publishEndpoint.Publish(new VideoProcessingProgressEvent(context.Message.VideoId, context.Message.UserId, progressInPercents));
                    })
                    .ProcessAsynchronously();

                await publishEndpoint.Publish(new VideoProcessedEvent { FilePath = outputPath, VideoId = context.Message.VideoId, UserId = context.Message.UserId });

                _logger.LogInformation("Successfully saved to {Output}", outputPath);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to convert file: {Error} ", ex.Message);
                await publishEndpoint.Publish(new VideoProcessingFailedEvent(context.Message.VideoId, context.Message.UserId, ex.Message));
                throw;
            }
            finally
            {
                File.Delete(inputPath);
            }
        }

        private async Task ValidateVideoAsync(string filePath)
        {
            IMediaAnalysis mediaInfo;
            try
            {
                // 2. Probe actual file contents
                mediaInfo = await FFProbe.AnalyseAsync(filePath);
            }
            catch
            {
                throw new Exception("Файл не є відео");
            }

            if (mediaInfo.VideoStreams.Count == 0)
                throw new Exception("Відео не має відеопотоків");

            if (mediaInfo.Duration <= TimeSpan.Zero)
                throw new Exception("Відео не може бути 0 секунд довжиною");

            if (mediaInfo.Duration > TimeSpan.FromHours(3))
                throw new Exception("Відео не може бути довше ніж 3 години");
        }
    }
}
