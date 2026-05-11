using Microsoft.AspNetCore.Http;

namespace Application.Dtos.User;

public class UpdateUserDTO
{
    public IFormFile? Avatar { get; set; }
    public string? Bio { get; set; }
}