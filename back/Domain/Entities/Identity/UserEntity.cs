using Domain.Entities.Comment;
using Domain.Entities.Conversation;
using Domain.Entities.Favorite;
using Domain.Entities.Like;
using Domain.Entities.Message;
using Domain.Entities.Video;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

public class UserEntity : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public String? LastName { get; set; }

    public String? FirstName { get; set; }

    public String? Description { get; set; }

    public String? Avatar { get; set; }

    public int RefreshTokenVersion { get; set; }

    public DateTime? LastConfirmationEmailSentAt { get; set; }

    public ICollection<VideoEntity> Videos { get; set; } = new List<VideoEntity>();

    public ICollection<UserFollowEntity> Following { get; set; } = new List<UserFollowEntity>();

    public ICollection<UserFollowEntity> Followers { get; set; } = new List<UserFollowEntity>();

    public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

    public ICollection<LikeEntity> Likes { get; set; } = new List<LikeEntity>();

    public virtual ICollection<UserRoleEntity>? UserRoles { get; set; }

    public ICollection<FavoriteEntity> Favorites { get; set; } = new List<FavoriteEntity>();

    public ICollection<ConversationParticipant> ConversationParticipants { get; set; } = [];

    public ICollection<MessageEntity> SentMessages { get; set; } = [];

    public ICollection<CommentLikeEntity> LikedComments { get; set; } = new List<CommentLikeEntity>();
}