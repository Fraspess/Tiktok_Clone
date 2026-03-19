using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.DAL.Entities.User;
using Tiktok_Clone.DAL.Entities.Video;

namespace Tiktok_Clone.DAL.Entities.Comment
{
    public class CommentEntity : BaseEntity<String>
    {
        public override String Id { get; set; } = Guid.NewGuid().ToString();

        public required String Text { get; set; }

        public required String UserId { get; set; }

        public UserEntity? Author { get; set; }

        public required String VideoId { get; set; }

        public VideoEntity? Video { get; set; }
    }
}
