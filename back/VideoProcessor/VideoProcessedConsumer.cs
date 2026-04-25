using Contracts.Events;
using FFMpegCore;
using MassTransit;

namespace VideoProcessor
{
    internal class VideoProcessedConsumer(ILogger<VideoProcessedConsumer> _logger, IConfiguration config) : IConsumer<VideoProcessedEvent>
    {

        public async Task Consume(ConsumeContext<VideoProcessedEvent> context)
        {
            var inputPath = context.Message.FilePath;
            if (!File.Exists(inputPath))
            {
                _logger.LogError("Input File {ErrorInput} wasnt found ", inputPath);
                throw new Exception("File wasn't found");
            }
            ;

            var outputPath = Path.GetFullPath(
                Path.Combine(Path.GetDirectoryName(inputPath)!, "..", config["OutputVideoPath"]!, $"{Guid.NewGuid().ToString()}.mp4")
            );

            var outputDir = Path.GetDirectoryName(outputPath)!;
            Directory.CreateDirectory(outputDir);

            _logger.LogInformation("OUTPUT PATH: {OutputPath}", outputPath);
            _logger.LogInformation("Starting converting to mp4 at {Input}", inputPath);

            try
            {
                await FFMpegArguments
                    .FromFileInput(inputPath)
                    .OutputToFile(outputPath, overwrite: true, options => options
                        .WithVideoCodec("libx264")
                        .WithAudioCodec("aac")
                        .WithFastStart())
                    .ProcessAsynchronously();

                File.Delete(inputPath);

                _logger.LogInformation("Successfully saved to {Output}", outputPath);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to convert file: {Error} ", ex.Message);
                throw;
            }
        }
    }
}
