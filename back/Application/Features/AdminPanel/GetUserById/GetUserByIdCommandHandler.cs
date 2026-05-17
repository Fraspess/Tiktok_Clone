using Application.Dtos.User;
using AutoMapper;
using Domain.Entities.Identity;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AdminPanel.GetUserById;

public class GetUserByIdCommandHandler(UserManager<UserEntity> userManager, IMapper mapper) : IRequestHandler<GetUserByIdCommand, SimpleUserDTO>
{
    public async Task<SimpleUserDTO> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
                   ?? throw new NotFoundException("Користувача не знайдено");
        return mapper.Map<SimpleUserDTO>(user);
    }
}