namespace Contracts.Events
{
    public record VideoProcessingFailedEvent(Guid Id, Guid UserId, string Error);
}