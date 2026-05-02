namespace Contracts.Events
{
    public record VideoProcessingProgressEvent(Guid VideoId, Guid UserId, int Progress);
}