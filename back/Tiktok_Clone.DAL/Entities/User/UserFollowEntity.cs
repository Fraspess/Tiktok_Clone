using System;
using System.Collections.Generic;
using System.Text;

namespace Tiktok_Clone.DAL.Entities.User
{
    public class UserFollowEntity
    {
        public String FollowerId { get; set; } = null!;
        public UserEntity Follower { get; set; } = null!;

        public String FollowingId { get; set; } = null!;
        public UserEntity Following { get; set; } = null!;

        public DateTime FollowedAt { get; set; } = DateTime.UtcNow;

    }
}
