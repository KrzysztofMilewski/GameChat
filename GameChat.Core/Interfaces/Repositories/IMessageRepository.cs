using GameChat.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        Task AddMessageAsync(Message message);
        Task<IEnumerable<Message>> GetMessagesForConversationAsync(int conversationId);

        Task<UnreadMessage> GetUnreadMarkAsync(int messageId, int userId);
        Task<IEnumerable<UnreadMessage>> GetUnreadMessagesAsync(int userId);
        Task CreateUnreadMarksAsync(UnreadMessage unreadMessage);
        void DeleteUnreadMark(UnreadMessage unreadMessageMark);
        void DeleteUnreadMarks(int conversationId, int userId);
    }
}
