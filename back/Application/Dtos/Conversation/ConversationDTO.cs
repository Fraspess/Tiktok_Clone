using Application.Dtos.User;

namespace Application.Dtos.Conversation
{
    public class ConversationDTO
    {
        public Guid Id { get; set; }
        public List<SimpleUserDTO> Participants { get; set; } = [];
    }
}
