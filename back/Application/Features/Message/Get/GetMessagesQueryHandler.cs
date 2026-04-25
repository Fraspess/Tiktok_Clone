using Application.Dtos.Message;
using Application.Extensions;
using Application.Interfaces;
using Application.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Message.Get
{
    public class GetMessagesQueryHandler(IUnitOfWork _uow, IMapper _mapper) : IRequestHandler<GetMessagesQuery, PagedResult<MessageDTO>>
    {
        public async Task<PagedResult<MessageDTO>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var conversationExists = await _uow.Conversations.GetAll().AnyAsync(u => u.Id == request.ConversationId);
            if (!conversationExists) throw new NotFoundException("Чат не знайдено");

            var messages = await _uow.Messages
                .GetAll()
                .Where(m => m.ConversationId == request.ConversationId)
                .OrderByDescending(m => m.CreatedAt)
                .ProjectTo<MessageDTO>(_mapper.ConfigurationProvider)
                .ToPagedResultAsync(request.Settings);

            return messages;
        }
    }
}
