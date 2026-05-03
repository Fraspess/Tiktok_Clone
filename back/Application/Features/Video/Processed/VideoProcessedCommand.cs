using MediatR;

namespace Application.Features.Video.Processed
{
    public record VideoProcessedCommand(Guid VideoId, Guid UserId) : IRequest<Unit>;
}