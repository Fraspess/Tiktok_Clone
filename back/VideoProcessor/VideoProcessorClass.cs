namespace VideoProcessor
{
    public class VideoProcessorClass
    {
        //private async Task<string> SaveVideoFileAsync(String pathFile)
        //{
        //    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Videos");
        //    Directory.CreateDirectory(uploadFolder);

        //    var fileName = Guid.NewGuid().ToString();
        //    var inputPath = Path.Combine(uploadFolder, file.FileName);
        //    var outputPath = Path.Combine(uploadFolder, fileName + ".mp4");
        //    // зберігаємо исходник
        //    await using (var stream = File.Create(inputPath))
        //        await file.CopyToAsync(stream);
        //    try
        //    {
        //        // отримуємо інформацію про відео(медіа) кине exception якщо не media
        //        var mediaInfo = await FFmpeg.GetMediaInfo(inputPath);

        //        var videoStream = mediaInfo.VideoStreams.FirstOrDefault()?.SetCodec(VideoCodec.LibX265);
        //        var audioStream = mediaInfo.AudioStreams.FirstOrDefault()?.SetCodec(AudioCodec.Aac);

        //        // конвертуємо відео в mp4
        //        var conversion = FFmpeg.Conversions.New().SetOutput(outputPath);
        //        conversion.AddStream(videoStream);
        //        // аудіо потік може бути null 
        //        if (audioStream != null) conversion.AddStream(audioStream);

        //        await conversion.Start();

        //        // удаляємо исходник
        //        File.Delete(inputPath);
        //        // вертаємо назву файла і розширення щоб зберегти в БД
        //        return fileName + ".mp4";

        //    }
        //    catch (ValidationException)
        //    {
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (File.Exists(inputPath))
        //            File.Delete(inputPath);
        //    }
        //}
    }
}
