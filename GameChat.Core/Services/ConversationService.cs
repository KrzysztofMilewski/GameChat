using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Interfaces.Services;
using System.Linq;
using System.Threading.Tasks;

namespace GameChat.Core.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConversationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> CreateNewConversation(ConversationDto conversation)
        {
            if (string.IsNullOrWhiteSpace(conversation.Title))
                return new ServiceResult(false, "Conversation title cannot be empty. Please specify the title");

            if (conversation.Participants.Count < 2)
                return new ServiceResult(false, "Conversation must have at least 2 participants");

            var participantsIds = conversation.Participants.Select(p => p.Id).ToArray();

            await _unitOfWork.ConversationRepository.CreateNewConversationAsync(conversation.Title, participantsIds);
            await _unitOfWork.CompleteTransactionAsync();

            return new ServiceResult(true, "Conversation created successfully");
        }
    }
}
