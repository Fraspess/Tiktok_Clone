using MediatR;
using Tiktok_Clone.BLL.Commands.Video;
using Tiktok_Clone.BLL.Services.Video;

namespace Tiktok_Clone.BLL.Handlers.Video
{
    public class CreateVideoCommandHandler(IVideoService videoService) : IRequestHandler<CreateVideoCommand, Unit>
    {
        public async Task<Unit> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            await videoService.UploadVideoAsync(request.Dto, request.OwnerId);
            return Unit.Value;
        }
    }
}
