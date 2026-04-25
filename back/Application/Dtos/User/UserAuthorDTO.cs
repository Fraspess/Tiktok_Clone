namespace Application.Dtos.User
{
    public class UserAuthorDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
    }
}
