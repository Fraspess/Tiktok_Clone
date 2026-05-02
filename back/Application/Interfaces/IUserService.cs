using Application.Dtos.Token;
using Application.Dtos.User;

namespace Application.Interfaces;

public interface IUserService
{
    Task<TokenResponseDTO> Login(LoginUserDTO dto);

    Task Register(RegisterUserDTO dto);

    Task<TokenResponseDTO> ConfirmEmail(string email, string token);

    Task<UserMeDTO> GetCurrentUserAsync(Guid userId);

    Task UpdateTokenVersion(Guid userId);

    Task ForgotPasswordAsync(string email);

    Task ResetPasswordAsync(ResetPasswordDTO dto);

    Task ResendConfirmationEmailAsync(string email);

    Task<UserDTO> GetByUsernameAsync(string username, Guid? currentUserId);

    Task ToggleFollowAsync(Guid follower, Guid following);

    Task<TokenResponseDTO> GoogleAuth(string idToken);

    Task<bool> IsExistsById(Guid id);
}