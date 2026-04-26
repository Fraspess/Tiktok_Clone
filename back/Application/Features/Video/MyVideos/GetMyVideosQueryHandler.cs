using Application.Dtos.Video;
using Application.Extensions;
using Application.Interfaces;
using Application.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Features.Video.MyVideos
{
    internal class GetMyVideosQueryHandler(IUnitOfWork _uow, IMapper _mapper) : IRequestHandler<GetMyVideosQuery, PagedResult<VideoDTO>>
    {
        public Task<PagedResult<VideoDTO>> Handle(GetMyVideosQuery request, CancellationToken cancellationToken)
        {
            var videos = _uow.Videos
                .GetAllIgnoreQueryFilters()
                .Where(v => v.UserId == request.UserId)
                .OrderByDescending(v => v.CreatedAt)
                .ProjectTo<VideoDTO>(_mapper.ConfigurationProvider)
                .ToPagedResultAsync(request.Settings);
            return videos;
        }
    }
}
