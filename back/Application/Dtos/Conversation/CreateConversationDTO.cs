namespace Application.Dtos.Conversation
{
    public class CreateConversationDTO
    {
        public List<Guid> UserIds { get; set; } = [];
    }
}