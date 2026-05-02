using Application.Dtos.Conversation;
using Application.Dtos.Message;
using Application.Dtos.User;
using AutoMapper;
using Domain.Entities.Conversation;
using Domain.Entities.Message;

namespace Application.MapperProfiles.Conversations
{
    public class ConversationMapperProfile : Profile
    {
        public ConversationMapperProfile()
        {
            CreateMap<ConversationEntity, ConversationDTO>()
                .ForMember(c => c.Participants, o => o.MapFrom(c => c.Participants));

            CreateMap<MessageEntity, MessageDTO>()
                .ForMember(c => c.SenderUsername, o => o.MapFrom(o => $"@{o.Sender.UserName}"));

            CreateMap<ConversationParticipant, SimpleUserDTO>()
                .ForMember(u => u.Username, o => o.MapFrom(p => $"@{p.User.UserName}"))
                .ForMember(u => u.Avatar, o => o.MapFrom(p => p.User.Avatar));
        }
    }
}