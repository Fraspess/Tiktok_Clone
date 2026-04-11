namespace Tiktok_Clone.BLL.Features.User.ResetPassword
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; } = String.Empty;

        public string NewPassword { get; set; } = String.Empty;

        public string Token { get; set; } = String.Empty;
    }
}
