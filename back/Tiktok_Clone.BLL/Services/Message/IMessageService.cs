namespace Tiktok_Clone.BLL.Services.Message
{
    public interface IMessageService
    {
        public Task SendAsync(Guid userId, Guid conversationId, string content);
        public Task FlushPendingAsync(Guid userId);

    }
}
