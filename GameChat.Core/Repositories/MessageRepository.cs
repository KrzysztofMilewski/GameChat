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

        public MessageRepository(ApplicationDbContext context)
        {
            _messages = context.Set<Message>();
        }

        public async Task AddMessageAsync(Message message)
        {
            await _messages.AddAsync(message);
        }

        public async Task<IEnumerable<Message>> GetMessagesForConversation(int conversationId)
        {
            return await _messages.Where(m => m.ConversationId == conversationId).OrderBy(m => m.DateSent).ToListAsync();
        }
    }
}
