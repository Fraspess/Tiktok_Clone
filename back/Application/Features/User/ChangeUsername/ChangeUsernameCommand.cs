using MediatR;

namespace Application.Features.User.ChangeUsername;

public record ChangeUsernameCommand(string newUsername, Guid userId) : IRequest<Unit>;