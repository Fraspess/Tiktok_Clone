using Tiktok_Clone.BLL.Dtos.Token;
using Tiktok_Clone.BLL.Dtos.User;

namespace Tiktok_Clone.BLL.Services.User;

public interface IUserService
{
    Task<TokenResponseDTO> Login(LoginUserDTO dto);

    Task<TokenResponseDTO> Register(RegisterUserDTO dto);

    Task<UserDTO> GetCurrentUserAsync(string userId);

    Task UpdateTokenVersion(string userId);

    Task ForgotPasswordAsync(string email);
}