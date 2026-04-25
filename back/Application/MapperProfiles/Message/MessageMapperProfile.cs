using Application.Dtos.Message;
using AutoMapper;
using Domain.Entities.Message;

namespace Application.MapperProfiles.Message
{
    public class MessageMapperProfile : Profile
    {
        public MessageMapperProfile()
        {

            CreateMap<MessageEntity, MessageDTO>();
        }
    }
}
