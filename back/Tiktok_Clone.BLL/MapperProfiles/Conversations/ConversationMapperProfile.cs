using AutoMapper;
using Tiktok_Clone.BLL.Dtos.Conversation;
using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.DAL.Entities.Conversation;
using Tiktok_Clone.DAL.Entities.Message;

namespace Tiktok_Clone.BLL.MapperProfiles.Conversations
{
    public class ConversationMapperProfile : Profile
    {
        public ConversationMapperProfile()
        {
            CreateMap<ConversationEntity, ConversationDTO>()
                .ForMember(c => c.Participants, o => o.MapFrom(c => c.Participants));

            CreateMap<MessageEntity, MessageDTO>();

            CreateMap<ConversationParticipant, SimpleUserDTO>()
                .ForMember(u => u.Username, o => o.MapFrom(p => $"@{p.User.UserName}"))
                .ForMember(u => u.Avatar, o => o.MapFrom(p => p.User.Avatar));
        }
    }
}
