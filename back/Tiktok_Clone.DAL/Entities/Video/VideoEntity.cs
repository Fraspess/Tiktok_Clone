using System;
using System.Collections.Generic;
using System.Text;
using Tiktok_Clone.DAL.Entities.Comment;
using Tiktok_Clone.DAL.Entities.HashTags;
using Tiktok_Clone.DAL.Entities.Identity;
using Tiktok_Clone.DAL.Entities.Like;

namespace Tiktok_Clone.DAL.Entities.Video
{
    public class VideoEntity : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();

        public required String Name { get; set; }

        public required String Description { get; set; }

        public required Guid UserId { get; set; }

        public UserEntity? Author { get; set; }

        public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

        public ICollection<VideoHashTagEntity> HashTags { get; set; } = new List<VideoHashTagEntity>();

        public ICollection<LikeEntity> Likes { get; set; } = new List<LikeEntity>();
    }
}
