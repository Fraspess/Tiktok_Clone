using Application.Features.Video.Shared;
using Application.Interfaces;
using Application.Services.HashTag;
using Contracts.Events;
using Domain.Entities.Video;
using MediatR;

namespace Application.Features.Video.Upload
{
    internal class UploadVideoCommandHandler(IUnitOfWork _uow, IDescriptionParser _parser, IHashTagService _hashtag, IEventBus<VideoProcessedEvent> eventBus, ITempVideoStorage tempVideoStorage) : IRequestHandler<UploadVideoCommand, Unit>
    {
        public async Task<Unit> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
        {

            var parsedDescription = _parser.ParseDescription(request.Dto.Description);
            var newVideo = new VideoEntity()
            {
                UserId = request.OwnerId,
                Description = parsedDescription.CleanText,
                Status = "Processing",
                ProccessedInProcents = 0
            };

            var hashtags = await _hashtag.GetOrCreateAsync(parsedDescription.Tags);
            foreach (var tag in hashtags)
                newVideo.HashTags.Add(new VideoHashTagEntity { HashTagId = tag.Id, VideoId = newVideo.Id });

            await _uow.Videos.CreateAsync(newVideo);
            await _uow.SaveChangesAsync();

            var tempFilePath = await tempVideoStorage.SaveVideoAsync(request.Dto.VideoFile);
            await eventBus.PublishAsync(new VideoProcessedEvent { FilePath = tempFilePath });
            return Unit.Value;
        }
    }
}
