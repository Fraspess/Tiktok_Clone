using MediatR;

namespace Application.Features.AdminPanel.BanUser;

public record BanUserCommand(Guid Id, string Reason, Guid CurrentUserId) : IRequest<Unit>;