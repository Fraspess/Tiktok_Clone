namespace Tiktok_Clone.BLL.Dtos.Message
{
    public class MessageDTO
    {
        Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string SenderUsername { get; set; } = String.Empty;
        public string SenderAvatar { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;

    }
}
