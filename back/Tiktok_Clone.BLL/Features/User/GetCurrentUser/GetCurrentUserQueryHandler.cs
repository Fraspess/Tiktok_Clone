using MediatR;
using Tiktok_Clone.BLL.Services.User;

namespace Tiktok_Clone.BLL.Features.User.GetCurrentUser
{
    public class GetCurrentUserQueryHandler(IUserService userService)
        : IRequestHandler<GetCurrentUserQuery, UserMeDTO>
    {
        public async Task<UserMeDTO> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            return await userService.GetCurrentUserAsync(request.Id);
        }
    }
}
