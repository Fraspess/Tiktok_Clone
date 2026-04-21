using AutoMapper;
using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.DAL.Entities.Message;

namespace Tiktok_Clone.BLL.MapperProfiles.Message
{
    public class MessageMapperProfile : Profile
    {
        public MessageMapperProfile()
        {

            CreateMap<MessageEntity, MessageDTO>();
        }
    }
}
