using Tiktok_Clone.BLL.Dtos.User;

namespace Tiktok_Clone.BLL.Services.User;

public interface IUserService
{
    Task<UserDTO> CreateUserAsync(CreateUserDTO user);
}