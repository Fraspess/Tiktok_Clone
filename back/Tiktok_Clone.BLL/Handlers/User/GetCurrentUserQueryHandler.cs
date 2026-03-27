using AutoMapper;
using MediatR;
using Tiktok_Clone.BLL.Dtos.User;
using Tiktok_Clone.BLL.Queries.User;
using Tiktok_Clone.BLL.Services.User;

namespace Tiktok_Clone.BLL.Handlers.User
{
    public class GetCurrentUserQueryHandler(IUserService userService, IMapper mapper)
        : IRequestHandler<GetCurrentUserQuery, UserDTO>
    {
        public async Task<UserDTO> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<UserDTO>(await userService.GetCurrentUserAsync(request.Id));
        }
    }
}
