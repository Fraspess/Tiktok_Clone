using MediatR;

namespace Tiktok_Clone.BLL.Features.User.ForgotPassword
{
    public record ForgotPasswordCommand(string email) : IRequest<Unit>;
}
