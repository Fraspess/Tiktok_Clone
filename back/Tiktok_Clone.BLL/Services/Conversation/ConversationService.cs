using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tiktok_Clone.BLL.Dtos.Conversation;
using Tiktok_Clone.BLL.Exceptions;
using Tiktok_Clone.BLL.Extensions;
using Tiktok_Clone.BLL.Pagination;
using Tiktok_Clone.DAL.Entities.Conversation;
using Tiktok_Clone.DAL.Entities.Identity;
using Tiktok_Clone.DAL.UnitOfWork;

namespace Tiktok_Clone.BLL.Services.Conversation
{
    public class ConversationService(IUnitOfWork _uow, IMapper _mapper, UserManager<UserEntity> _userManager) : IConversationService
    {
        public async Task<ConversationDTO> CreateConversationAsync(Guid currentUserId, List<Guid> participants)
        {
            if (!participants.Contains(currentUserId))
            {
                participants.Add(currentUserId);
            }

            foreach (var participant in participants)
            {
                var _ = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == participant)
                    ?? throw new NotFoundException("Користувача з таким id не знайдено");
            }

            var conversation = new ConversationEntity
            {
                Participants = participants.Select(id => new ConversationParticipant
                {
                    UserId = id,
                }).ToList()
            };

            await _uow.Conversations.CreateAsync(conversation);
            await _uow.SaveChangesAsync();

            return _mapper.Map<ConversationDTO>(conversation);
        }

        public async Task<ConversationDTO> GetConversationAsync(Guid conversationId, Guid userId)
        {
            var conversation = await _uow.Conversations.GetByIdAsync(conversationId)
                ?? throw new NotFoundException("Бесіда не знайдена");

            if (!conversation.Participants.Any(p => p.Id == conversation.Id))
            {
                throw new NotAllowedException("Ви не маєте прав на перегляд цієї сторінки.");
            }

            var dto = _mapper.Map<ConversationDTO>(conversation);
            return dto;
        }

        public async Task<PagedResult<ConversationDTO>> GetConversationsAsync(Guid userId, PaginationSettings settings)
        {
            var convo = await _uow.Conversations
                .GetAll()
                .AsSplitQuery()
                .Where(c => c.Participants.Any(p => p.UserId == userId))
                .OrderByDescending(x => x.CreatedAt)
                .ProjectTo<ConversationDTO>(_mapper.ConfigurationProvider)
                .ToPagedResultAsync(settings);


            return convo;
        }
    }
}
