using Application.Dtos.Video;
using Application.Extensions;
using Application.Interfaces;
using Application.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Video.MyVideos
{
    internal class GetMyVideosQueryHandler(IUnitOfWork _uow, IMapper _mapper, IConfiguration config)
        : IRequestHandler<GetMyVideosQuery, PagedResult<MyVideoDTO>>
    {
        public Task<PagedResult<MyVideoDTO>> Handle(GetMyVideosQuery request, CancellationToken cancellationToken)
        {
            var videos = _uow.Videos
                .GetAllIgnoreQueryFilters()
                .Where(v => v.UserId == request.UserId)
                .OrderByDescending(v => v.CreatedAt)
                .ProjectTo<MyVideoDTO>(_mapper.ConfigurationProvider,
                    new { userId = request.UserId , backendUrl = config["Backend:Url"]})
                .ToPagedResultAsync(request.Settings);
            return videos;
        }
    }
}