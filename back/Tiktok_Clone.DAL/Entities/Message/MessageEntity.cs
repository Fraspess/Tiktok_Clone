using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.DAL.Entities.Identity;

namespace Tiktok_Clone.DAL.Entities.Message
{
    public class MessageEntity : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();

        public required Guid SenderId { get; set; }
        public UserEntity Sender { get; set; } = null!;

        public required Guid ReceiverId { get; set; }
        public UserEntity Receiver { get; set; } = null!;

        public required String Text { get; set; }


    }
}
