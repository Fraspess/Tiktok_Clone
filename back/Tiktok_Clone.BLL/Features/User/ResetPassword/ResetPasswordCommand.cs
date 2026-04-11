using MediatR;

namespace Tiktok_Clone.BLL.Features.User.ResetPassword
{
    public record ResetPasswordCommand(string Email, string Token, string NewPassword) : IRequest<Unit>;

}
