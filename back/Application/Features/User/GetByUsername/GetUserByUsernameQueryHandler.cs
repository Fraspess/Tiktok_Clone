using Application.Dtos.User;
using Application.Interfaces;
using MediatR;

namespace Application.Features.User.GetByUsername
{
    public class GetUserByUsernameQueryHandler(IUserService service) : IRequestHandler<GetUserByUsernameQuery, UserDTO>
    {
        public async Task<UserDTO> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            return await service.GetByUsernameAsync(request.Username, request.CurrentUserId);
        }
    }
}
