namespace Contracts.Events;

public class VideoProcessedEvent
{
    public Guid Id { get; set; }
    public string FilePath { get; set; } = string.Empty;
}
