using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.DAL.Entities.User;

namespace Tiktok_Clone.BLL.Services.User;

public interface IUserService
{
    Task<UserDTO> CreateUserAsync(CreateUserDTO user);
}