using GameChat.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Services
{
    public interface IMessageService
    {
        Task SendMessage(MessageDto messageDto);
        Task<IEnumerable<MessageDto>> GetMessagesForConversation(int conversationId);
    }
}
