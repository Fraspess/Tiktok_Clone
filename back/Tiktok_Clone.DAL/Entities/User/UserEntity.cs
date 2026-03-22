using Microsoft.AspNetCore.Identity;
using Tiktok_Clone.DAL.Entities.Comment;
using Tiktok_Clone.DAL.Entities.Like;
using Tiktok_Clone.DAL.Entities.Message;
using Tiktok_Clone.DAL.Entities.Video;

namespace Tiktok_Clone.DAL.Entities.User;

public class UserEntity : IdentityUser
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<VideoEntity> Videos { get; set; } = new List<VideoEntity>();

    public ICollection <UserFollowEntity> Following { get; set; } = new List<UserFollowEntity>();

    public ICollection<UserFollowEntity> Followers { get; set; } = new List<UserFollowEntity>();

    public ICollection<MessageEntity> SentMessages { get; set; } = new List<MessageEntity>();
    public ICollection<MessageEntity> ReceivedMessages { get; set; } = new List<MessageEntity>();


    public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

    public ICollection<LikeEntity> Likes { get; set; } = new List<LikeEntity>();
}   