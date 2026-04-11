using MediatR;

namespace Tiktok_Clone.BLL.Features.User.ResendConfirmationEmail
{
    public record ResendConfirmationEmailCommand(string Email) : IRequest<Unit>;

}
