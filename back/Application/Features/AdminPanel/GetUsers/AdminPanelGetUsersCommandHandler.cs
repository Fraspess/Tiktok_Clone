using Application.Dtos.User;
using Application.Extensions;
using Application.Interfaces;
using Application.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.AdminPanel.GetUsers;

public class AdminPanelGetUsersCommandHandler(UserManager<UserEntity> userManager, IMapper mapper) : IRequestHandler<AdminPanelGetUsersCommand, PagedResult<SimpleUserDTO>>
{
    public Task<PagedResult<SimpleUserDTO>> Handle(AdminPanelGetUsersCommand request, CancellationToken cancellationToken)
    {
        return userManager.Users
            .ProjectTo<SimpleUserDTO>(mapper.ConfigurationProvider)
            .ToPagedResultAsync(request.PaginationSettings);
    }
}