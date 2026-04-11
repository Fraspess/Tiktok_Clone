using MediatR;

namespace Tiktok_Clone.BLL.Features.User.GetCurrentUser
{
    public record GetCurrentUserQuery(Guid Id) : IRequest<UserMeDTO>;

}
