using GameChat.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Repositories
{
    public interface IConversationRepository
    {
        Task CreateNewConversationAsync(Conversation conversation);
        Task<bool> IsUserParticipatingAsync(int conversationId, int senderId);
        Task<IEnumerable<ConversationParticipant>> GetParticipantsAsync(int conversationId);
        Task<IEnumerable<User>> GetParticipantsAsUsersAsync(int conversationId);
        Task<IEnumerable<Conversation>> GetConversationsForUser(int userId);
    }
}
