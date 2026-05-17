using Application.Interfaces;
using Domain.Entities.Identity;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AdminPanel.BanUser;

public class BanUserCommandHandler(UserManager<UserEntity> userManager) : IRequestHandler<BanUserCommand, Unit>
{
    public async Task<Unit> Handle(BanUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException("Користувача не знайдено");

        user.BannedBy = request.CurrentUserId;
        user.BannedAt = DateTime.UtcNow;
        await userManager.UpdateAsync(user);
        return Unit.Value;
    }
}