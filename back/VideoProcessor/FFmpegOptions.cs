namespace VideoProcessor;

public class FFmpegOptions
{
    public List<QualityOptions> Qualities { get; set; }
    public EncodingOptions Encoding { get; set; }
}

public class QualityOptions
{
    public int Quality { get; set; }
    public string Scale { get; set; }
    public string VideoBitrate { get; set; }
    public string MaxRate { get; set; }
    public string BuffSize { get; set; }
    public string AudioBitrate { get; set; }
    public int Bandwidth { get; set; }
}

public class EncodingOptions
{
    public string VideoCodec { get; set; }
    public string AudioCodec { get; set; }
    public string Preset { get; set; }
    public int Crf { get; set; }
}