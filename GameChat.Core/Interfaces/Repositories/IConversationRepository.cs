using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Repositories
{
    public interface IConversationRepository
    {
        Task CreateNewConversationAsync(string title, int[] participantsIds);
        Task<bool> IsUserParticipatingAsync(int conversationId, int senderId);
    }
}
