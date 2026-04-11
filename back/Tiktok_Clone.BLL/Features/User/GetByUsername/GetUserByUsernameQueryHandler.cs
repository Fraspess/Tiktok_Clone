using MediatR;
using Tiktok_Clone.BLL.Services.User;

namespace Tiktok_Clone.BLL.Features.User.GetByUsername
{
    public class GetUserByUsernameQueryHandler(IUserService service) : IRequestHandler<GetUserByUsernameQuery, UserDTO>
    {
        public async Task<UserDTO> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            return await service.GetByUsernameAsync(request.Username, request.CurrentUserId);
        }
    }
}
