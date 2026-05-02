using Application.Dtos.User;
using MediatR;

namespace Application.Features.User.GetByUsername
{
    public record GetUserByUsernameQuery(string Username, Guid? CurrentUserId) : IRequest<UserDTO>;
}