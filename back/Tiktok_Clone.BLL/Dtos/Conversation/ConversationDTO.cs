using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.BLL.Dtos.User;

namespace Tiktok_Clone.BLL.Dtos.Conversation
{
    public class ConversationDTO
    {
        public Guid Id { get; set; }
        public List<SimpleUserDTO> Participants { get; set; } = [];
        public List<MessageDTO> Messages { get; set; } = new List<MessageDTO>();
    }
}
