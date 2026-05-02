using Application.Dtos.Video;
using Application.Extensions;
using Application.Interfaces;
using Application.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Video.GetBySomeQuery
{
    public class GetVideosBySomeStringQueryHandler(IUnitOfWork _uow, IMapper _mapper)
        : IRequestHandler<GetVideosBySomeStringQuery, PagedResult<SimpleVideoDTO>>
    {
        public async Task<PagedResult<SimpleVideoDTO>> Handle(GetVideosBySomeStringQuery request,
            CancellationToken cancellationToken)
        {
            var someString = request.SomeString.ToLower().Trim();
            var query = _uow.Videos
                .GetAll()
                .Include(v => v.HashTags)
                .Include(v => v.Author)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(someString))
            {
                query = query.Where(v =>
                    v.Description.ToLower().Contains(someString) ||
                    v.Author!.UserName!.ToLower().Contains(someString) ||
                    v.HashTags.Any(h => h.HashTag.Tag.ToLower().Contains(someString))
                );
            }

            var videos = await query
                .OrderByDescending(v => v.CreatedAt)
                .ProjectTo<SimpleVideoDTO>(_mapper.ConfigurationProvider)
                .ToPagedResultAsync(request.Settings);

            return videos;
        }
    }
}