using Domain.Entities.Identity;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User.ChangeUsername;

internal class ChangeUsernameCommandHandler(UserManager<UserEntity> userManager) : IRequestHandler<ChangeUsernameCommand, Unit>
{
    public async Task<Unit> Handle(ChangeUsernameCommand request, CancellationToken cancellationToken)
    {
        var id = request.userId;

        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == request.userId)
            ?? throw new NotFoundException("Користувача не знайдено");
        
        if (user.LastUsernameChangedAt.HasValue && (DateTime.Now <=  user.LastUsernameChangedAt.Value.AddDays(30)))
        {
            throw new BadRequestException("Ви можете міняти ім'я користувача лише 1 раз в 30 днів");
        }

        if (await userManager.FindByNameAsync(request.newUsername) is not null)
            throw new BadRequestException("Ім'я користувача уже зайнято");
        
        user.UserName = request.newUsername;
        user.LastUsernameChangedAt = DateTime.UtcNow;
        await userManager.UpdateAsync(user);
        return Unit.Value;
    }
}