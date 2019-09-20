using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Services
{
    public interface IMessageService
    {
        Task<ServiceResult<MessageDto>> SendMessageAsync(MessageDto messageDto);
        Task<ServiceResult<IEnumerable<MessageDto>>> GetMessagesForConversationAsync(int conversationId, int requestingUserId);
        Task<ServiceResult> ReadMessageAsync(int messageId, int readingUserId);
    }
}
