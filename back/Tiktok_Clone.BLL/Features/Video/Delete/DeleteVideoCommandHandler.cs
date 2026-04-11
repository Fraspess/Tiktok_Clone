using MediatR;
using Tiktok_Clone.BLL.Services.Video;

namespace Tiktok_Clone.BLL.Features.Video.Delete
{
    public class DeleteVideoCommandHandler(IVideoService videoService) : IRequestHandler<DeleteVideoCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            await videoService.DeleteVideoById(request.VideoId, request.UserId);
            return Unit.Value;
        }
    }
}
