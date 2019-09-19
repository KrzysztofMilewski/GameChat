using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Models;
using GameChat.Core.Persistence;
using Microsoft.EntityFrameworkCore;
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
            var user = await _conversationParticipants.SingleOrDefaultAsync(cp => cp.ConversationId == conversationId && cp.ParticipantId == senderId);

            return user == null ? false : true;
        }
    }
}
