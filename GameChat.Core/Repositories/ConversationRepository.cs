using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Models;
using GameChat.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameChat.Core.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly DbSet<Conversation> _conversations;
        private readonly DbSet<ConversationParticipant> _conversationParticipants;

        public ConversationRepository(ApplicationDbContext context)
        {
            _conversations = context.Set<Conversation>();
            _conversationParticipants = context.Set<ConversationParticipant>();
        }

        public async Task CreateNewConversationAsync(Conversation conversation)
        {
            await _conversations.AddAsync(conversation);
        }

        public async Task<bool> IsUserParticipatingAsync(int conversationId, int senderId)
        {
            var user = await _conversationParticipants.
                SingleOrDefaultAsync(cp => cp.ConversationId == conversationId && cp.ParticipantId == senderId);

            return user == null ? false : true;
        }

        public async Task<IEnumerable<ConversationParticipant>> GetParticipantsAsync(int conversationId)
        {
            return await _conversationParticipants.Where(cp => cp.ConversationId == conversationId).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetParticipantsAsUsersAsync(int conversationId)
        {
            return await _conversationParticipants.
                Include(c => c.Participant).
                Where(c => c.ConversationId == conversationId).
                Select(c => c.Participant).
                ToListAsync();
        }

        public async Task<IEnumerable<Conversation>> GetConversationsForUserAsync(int userId)
        {
            return await _conversationParticipants.
                Include(cp => cp.Conversation).
                Where(cp => cp.ParticipantId == userId).
                Select(c => c.Conversation).
                ToListAsync();
        }

        public async Task<Conversation> GetConversationAsync(int conversationId)
        {
            return await _conversations.SingleOrDefaultAsync(c => c.Id == conversationId);
        }
    }
}
