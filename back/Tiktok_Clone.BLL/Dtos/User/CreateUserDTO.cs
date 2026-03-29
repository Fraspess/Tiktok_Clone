namespace Tiktok_Clone.BLL.Dtos.User;

public class CreateUserDTO
{
    public required string Username { get; set; }

    public required string Password { get; set; }

    public required string Email { get; set; }
}