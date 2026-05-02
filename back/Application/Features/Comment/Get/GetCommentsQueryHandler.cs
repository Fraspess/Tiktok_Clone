using Application.Dtos.Comment;
using Application.Extensions;
using Application.Interfaces;
using Application.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Features.Comment.Get
{
    public class GetCommentsQueryHandler(IUnitOfWork _uow, IMapper _mapper)
        : IRequestHandler<GetCommentsQuery, PagedResult<CommentDTO>>
    {
        public async Task<PagedResult<CommentDTO>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Comments
                .GetCommentsByVideoId(request.VideoId)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .ToPagedResultAsync(request.PaginationSettings);
        }
    }
}