namespace Contracts.Events
{
    public class VideoProcessedEvent
    {
        public Guid VideoId { get; set; }
        public Guid UserId { get; set; }
        public string FilePath { get; set; } = string.Empty;
    }
}
