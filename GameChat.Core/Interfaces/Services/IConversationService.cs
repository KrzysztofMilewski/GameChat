using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using System.Threading.Tasks;

namespace GameChat.Core.Interfaces.Services
{
    public interface IConversationService
    {
        Task<ServiceResult> CreateNewConversation(ConversationDto conversation);
    }
}
