namespace Application.Dtos.Message
{
    public class MessageDTO
    {
        Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string SenderUsername { get; set; } = String.Empty;
        public string SenderAvatar { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; }

        public bool IsOwn { get; set; }
    }
}