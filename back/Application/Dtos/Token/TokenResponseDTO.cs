namespace Application.Dtos.Token
{
    public class TokenResponseDTO
    {
        public String AccessToken { get; set; } = String.Empty;

        public String RefreshToken { get; set; } = String.Empty;
    }
}
