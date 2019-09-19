using GameChat.Core.Models;
using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Repositories
{
    public interface IConversationRepository
    {
        Task CreateNewConversationAsync(Conversation conversation);
        Task<bool> IsUserParticipatingAsync(int conversationId, int senderId);
    }
}
