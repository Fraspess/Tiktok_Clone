using System.ComponentModel.DataAnnotations;
using Domain.Entities.Comment;
using Domain.Entities.Favorite;
using Domain.Entities.Identity;
using Domain.Entities.Like;

namespace Domain.Entities.Video
{
    public class VideoEntity : BaseEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
    
        [MaxLength(500)]
        public required string Description { get; set; }

        public required Guid UserId { get; set; }

        public UserEntity? Author { get; set; }

        public int ProccessedInProcents { get; set; } // returned from microservice
        public string Status { get; set; } = string.Empty; // processing, processed and etc

        public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

        public ICollection<VideoHashTagEntity> HashTags { get; set; } = new List<VideoHashTagEntity>();

        public ICollection<LikeEntity> Likes { get; set; } = new List<LikeEntity>();

        public ICollection<FavoriteEntity> Favorites { get; set; } = new List<FavoriteEntity>();
    }
}