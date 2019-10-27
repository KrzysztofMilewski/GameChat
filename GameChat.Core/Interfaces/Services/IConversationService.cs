using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Services
{
    public interface IConversationService
    {
        Task<ServiceResult<int>> CreateNewConversation(ConversationDto conversation);
        Task<ServiceResult<IEnumerable<ConversationDto>>> GetConversationsForUser(int userId);
        Task<ServiceResult<ConversationDto>> GetConversation(int conversationId, int requestingUserId);
        Task<ServiceResult<IEnumerable<UserDto>>> GetParticipants(int conversationId, int requestingUserId);
    }
}
