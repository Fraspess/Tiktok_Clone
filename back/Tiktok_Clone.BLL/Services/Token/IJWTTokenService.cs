using Tiktok_Clone.BLL.Dtos.Token;
using Tiktok_Clone.DAL.Entities.Identity;

namespace Tiktok_Clone.BLL.Services.Token
{
    public interface IJWTTokenService
    {
        Task<TokenResponseDTO> GenerateTokensAsync(UserEntity user);
    }
}
