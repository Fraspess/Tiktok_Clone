using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.DAL.Entities.User;

namespace Tiktok_Clone.DAL.Entities.Message
{
    public class MessageEntity : BaseEntity<String>
    {
        public override String Id { get; set; } = Guid.NewGuid().ToString();

        public required String SenderId { get; set; }
        public UserEntity Sender { get; set; } = null!;

        public required String ReceiverId { get; set; }
        public UserEntity Receiver { get; set; } = null!;

        public required String Text { get; set; }


    }
}
