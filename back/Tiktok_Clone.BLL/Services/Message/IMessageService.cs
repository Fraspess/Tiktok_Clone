using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.BLL.Dtos.Message;
using Tiktok_Clone.DAL.Entities.Message;

namespace Tiktok_Clone.BLL.Services.Message
{
    public interface IMessageService : IGenericService<MessageEntity, String, MessageDTO, CreateMessageDTO, UpdateMessageDTO>
    {
    }
}
