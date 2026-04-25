using Application.Dtos.Message;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.Message;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Message
{
    public class MessageService(IUnitOfWork _uow, IMapper _mapper, IChatNotifier _notifier) : IMessageService
    {
        public async Task FlushPendingAsync(Guid userId)
        {
            var pendingMessages = await _uow.Messages
                .GetAll()
                .Where(m =>
                    m.IsDelivered == false &&
                    m.SenderId != userId &&
                    m.Conversation.Participants
                        .Any(p => p.UserId == userId))
                .OrderBy(m => m.CreatedAt)
                .ProjectTo<MessageDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (!pendingMessages.Any()) return;

            await _notifier.SendPendingMessagesAsync(userId, pendingMessages);

        }

        public async Task MarkAsDeliveredAsync(Guid userId, Guid messageId)
        {
            var message = await _uow.Messages
                .GetAll()
                .FirstOrDefaultAsync(m =>
                    m.Id == messageId &&
                    m.Conversation.Participants.Any(p => p.UserId == userId))
            ?? throw new NotFoundException("Повідомлення не знайдено");

            message.IsDelivered = true;
            await _uow.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(Guid userId, Guid messageId)
        {
            var message = await _uow.Messages
                .GetAll()
                .FirstOrDefaultAsync(m =>
                    m.Id == messageId &&
                    m.Conversation.Participants.Any(p => p.UserId == userId))
                ?? throw new NotFoundException("Повідомлення не знайдено");

            message.IsRead = true;
            await _uow.SaveChangesAsync();
        }

        public async Task SendAsync(Guid userId, Guid conversationId, string content)
        {
            var conversationExists = await _uow.Conversations.GetAll().AnyAsync(u => u.Id == conversationId);
            if (!conversationExists) throw new NotFoundException("Чат не знайдено");

            var newMessage = new MessageEntity
            {
                SenderId = userId,
                ConversationId = conversationId,
                Content = content
            };

            await _uow.Messages.CreateAsync(newMessage);
            await _uow.SaveChangesAsync();
        }
    }
}
