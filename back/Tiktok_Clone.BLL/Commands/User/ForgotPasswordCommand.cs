using MediatR;

namespace Tiktok_Clone.BLL.Commands.User
{
    public record ForgotPasswordCommand(string email) : IRequest<Unit>;
}
