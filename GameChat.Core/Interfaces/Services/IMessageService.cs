using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Services
{
    public interface IMessageService
    {
        Task<ServiceResult> SendMessage(MessageDto messageDto);
        Task<ServiceResult<IEnumerable<MessageDto>>> GetMessagesForConversation(int conversationId, int requestingUserId);
    }
}
