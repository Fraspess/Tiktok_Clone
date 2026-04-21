using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tiktok_Clone.BLL.Dtos.Conversation;
using Tiktok_Clone.BLL.Dtos.Message;
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

            var existingConversation = await _uow.Conversations
                .GetAll()
                 .Include(c => c.Participants)
                .Where(c => c.Participants.Count == participants.Count &&
                    c.Participants.All(p => participants.Contains(p.UserId)))
                .FirstOrDefaultAsync();

            if (existingConversation is not null)
            {
                return _mapper.Map<ConversationDTO>(existingConversation);
            }

            foreach (var participant in participants)
            {
                var _ = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == participant)
                    ?? throw new BadRequestException("Користувача з таким id не знайдено");
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
            var conversation = await GetAuthorizedConversationAsync(conversationId, userId);

            var dto = _mapper.Map<ConversationDTO>(conversation);
            return dto;
        }

        public async Task<PagedResult<MessageDTO>> GetConversationMessagesAsync(Guid conversationId, PaginationSettings settings, Guid userId)
        {
            await GetAuthorizedConversationAsync(conversationId, userId);

            var messages = await _uow.Messages
                .GetAll()
                .Where(m => m.ConversationId == conversationId)
                .OrderByDescending(m => m.CreatedAt)
                .ProjectTo<MessageDTO>(_mapper.ConfigurationProvider, new { currentUserId = userId })
                .ToPagedResultAsync(settings);

            foreach (var message in messages.Items)
            {
                message.IsOwn = message.SenderId == userId;
            }
            return messages;
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


        private async Task<ConversationEntity> GetAuthorizedConversationAsync(Guid conversationId, Guid userId)
        {
            var conversation = await _uow.Conversations
                .GetAll()
                .Include(c => c.Participants)
                .FirstOrDefaultAsync(c => c.Id == conversationId)
                    ?? throw new NotFoundException("Розмову не знайдено");

            if (!conversation.Participants.Any(p => p.UserId == userId))
                throw new NotAllowedException("Ви не маєте прав на перегляд цієї сторінки.");

            return conversation;
        }
    }
}
