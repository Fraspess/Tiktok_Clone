using MediatR;

namespace Tiktok_Clone.BLL.Commands.User
{
    public record ResendConfirmationEmailCommand(string Email) : IRequest<Unit>;

}
