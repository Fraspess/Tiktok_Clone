using Tiktok_Clone.BLL.Dtos.Token;
using Tiktok_Clone.BLL.Dtos.User;

namespace Tiktok_Clone.BLL.Services.User;

public interface IUserService
{
    Task<TokenResponseDTO> Login(LoginUserDTO dto);

    Task Register(RegisterUserDTO dto);

    Task<TokenResponseDTO> ConfirmEmail(string email, string token);

    Task<UserDTO> GetCurrentUserAsync(Guid userId);

    Task UpdateTokenVersion(Guid userId);

    Task ForgotPasswordAsync(string email);

    Task ResetPasswordAsync(ResetPasswordDTO dto);

    Task ResendConfirmationEmailAsync(string email);
}