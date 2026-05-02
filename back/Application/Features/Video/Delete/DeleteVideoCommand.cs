using MediatR;

namespace Application.Features.Video.Delete
{
    public record DeleteVideoCommand(Guid VideoId, Guid UserId) : IRequest<Unit>;
}