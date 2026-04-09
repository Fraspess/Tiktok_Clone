using MediatR;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.BLL.Queries.User;
using Tiktok_Clone.BLL.Services.User;

namespace Tiktok_Clone.BLL.Handlers.User
{
    public class GetUserByUsernameQueryHandler(IUserService service) : IRequestHandler<GetUserByUsernameQuery, UserDTO>
    {
        public async Task<UserDTO> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            return await service.GetByUsernameAsync(request.Username, request.CurrentUserId);
        }
    }
}
