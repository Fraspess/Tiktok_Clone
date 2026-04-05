using MediatR;
using Tiktok_Clone.BLL.Commands.User;
using Tiktok_Clone.BLL.Services.User;

namespace Tiktok_Clone.BLL.Handlers.User
{
    public class ResendConfirmationEmailCommandHandler(IUserService service) : IRequestHandler<ResendConfirmationEmailCommand, Unit>
    {
        public async Task<Unit> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            await service.ResendConfirmationEmailAsync(request.Email);
            return Unit.Value;
        }
    }
}
