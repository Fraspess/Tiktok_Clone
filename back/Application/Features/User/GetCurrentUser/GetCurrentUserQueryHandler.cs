using Application.Dtos.User;
using Application.Interfaces;
using MediatR;

namespace Application.Features.User.GetCurrentUser
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