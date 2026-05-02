using Application.Dtos.Comment;
using Application.Extensions;
using Application.Interfaces;
using Application.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Features.Comment.GetReplies
{
    public class GetRepliesQueryHandler(IUnitOfWork _uow, IMapper _mapper)
        : IRequestHandler<GetRepliesQuery, PagedResult<CommentDTO>>
    {
        public async Task<PagedResult<CommentDTO>> Handle(GetRepliesQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Comments
                .GetRepliesAsync(request.ParentCommentId)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .ToPagedResultAsync(request.PaginationSettings);
        }
    }
}