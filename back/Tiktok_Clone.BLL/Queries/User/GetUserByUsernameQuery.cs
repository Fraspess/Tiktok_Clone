using MediatR;
using Tiktok_Clone.BLL.Dtos.User;

namespace Tiktok_Clone.BLL.Queries.User
{
    public record GetUserByUsernameQuery(string Username, Guid? CurrentUserId) : IRequest<UserDTO>;

}
