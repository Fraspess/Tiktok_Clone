namespace Tiktok_Clone.BLL.Features.User.GetCurrentUser
{
    public class UserMeDTO
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public bool IsOwnProfile { get; set; }
        public string Avatar { get; set; } = string.Empty;
    }
}
