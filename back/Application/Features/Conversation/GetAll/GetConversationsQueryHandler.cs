using Application.Dtos.Conversation;
using Application.Extensions;
using Application.Interfaces;
using Application.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Conversation.GetAll
{
    public class GetConversationsQueryHandler(IUnitOfWork _uow, IMapper _mapper)
        : IRequestHandler<GetConversationsQuery, PagedResult<ConversationDTO>>
    {
        public async Task<PagedResult<ConversationDTO>> Handle(GetConversationsQuery request,
            CancellationToken cancellationToken)
        {
            var convo = await _uow.Conversations
                .GetAll()
                .AsSplitQuery()
                .Where(c => c.Participants.Any(p => p.UserId == request.UserId))
                .OrderByDescending(x => x.CreatedAt)
                .ProjectTo<ConversationDTO>(_mapper.ConfigurationProvider)
                .ToPagedResultAsync(request.PaginationSettings);
            return convo;
        }
    }
}