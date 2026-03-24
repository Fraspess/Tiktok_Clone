using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.DAL.Entities.Identity;
using Tiktok_Clone.DAL.Entities.Video;

namespace Tiktok_Clone.DAL.Entities.Comment
{
    public class CommentEntity : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();

        public required String Text { get; set; }

        public required Guid UserId { get; set; }

        public UserEntity? Author { get; set; }

        public required Guid VideoId { get; set; }

        public VideoEntity? Video { get; set; }
    }
}
