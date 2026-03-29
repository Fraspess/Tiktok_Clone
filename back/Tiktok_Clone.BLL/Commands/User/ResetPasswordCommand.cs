using MediatR;

namespace Tiktok_Clone.BLL.Commands.User
{
    public record ResetPasswordCommand(string Email, string Token, string NewPassword) : IRequest<Unit>;

}
