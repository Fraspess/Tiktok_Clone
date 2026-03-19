using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.DAL.Entities.Message;
using Tiktok_Clone.DAL.Repositories;

namespace Tiktok_Clone.BLL.Services.Message
{
    public class MessageService : GenericService<MessageEntity, String, MessageDTO, CreateMessageDTO, UpdateMessageDTO>, IMessageService
    {
        public MessageService(IGenericRepository<MessageEntity, string> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
