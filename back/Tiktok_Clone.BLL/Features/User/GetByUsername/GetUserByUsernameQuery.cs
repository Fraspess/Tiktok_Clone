using MediatR;

namespace Tiktok_Clone.BLL.Features.User.GetByUsername
{
    public record GetUserByUsernameQuery(string Username, Guid? CurrentUserId) : IRequest<UserDTO>;

}
