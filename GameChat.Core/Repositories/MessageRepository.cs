using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Models;
using GameChat.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameChat.Core.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DbSet<Message> _messages;
        private readonly DbSet<UnreadMessage> _unreadMessages;

        public MessageRepository(ApplicationDbContext context)
        {
            _messages = context.Set<Message>();
            _unreadMessages = context.Set<UnreadMessage>();
        }

        public async Task AddMessageAsync(Message message)
        {
            await _messages.AddAsync(message);
        }

        public async Task<IEnumerable<Message>> GetMessagesForConversationAsync(int conversationId)
        {
            return await _messages.Where(m => m.ConversationId == conversationId).Include(m => m.Sender).OrderBy(m => m.DateSent).ToListAsync();
        }

        public async Task<UnreadMessage> GetUnreadMarkAsync(int messageId, int userId)
        {
            return await _unreadMessages.FirstOrDefaultAsync(um => um.MessageId == messageId && um.ParticipantId == userId);
        }

        public void DeleteUnreadMark(UnreadMessage unreadMessageMark)
        {
            _unreadMessages.Remove(unreadMessageMark);
        }

        public void DeleteUnreadMarks(int conversationId, int userId)
        {
            var unreadMessages = _unreadMessages.
                Include(um => um.Message).
                Where(um => um.Message.ConversationId == conversationId && um.ParticipantId == userId);

            _unreadMessages.RemoveRange(unreadMessages);
        }

        public async Task CreateUnreadMarksAsync(UnreadMessage unreadMessage)
        {
            await _unreadMessages.AddAsync(unreadMessage);
        }
    }
}
