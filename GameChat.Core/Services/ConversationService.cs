using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Interfaces.Services;
using GameChat.Core.Models;
using System.Collections.ObjectModel;
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

        public async Task<ServiceResult<int>> CreateNewConversation(ConversationDto conversationDto)
        {
            if (string.IsNullOrWhiteSpace(conversationDto.Title))
                return new ServiceResult<int>(false, "Conversation title cannot be empty. Please specify the title");

            if (conversationDto.Participants.Count < 2)
                return new ServiceResult<int>(false, "Conversation must have at least 2 participants");

            var participatingUsers = conversationDto.Participants.Select(u => new ConversationParticipant() { ParticipantId = u.Id }).ToList();

            var conversation = new Conversation()
            {
                Title = conversationDto.Title,
                Participants = new Collection<ConversationParticipant>(participatingUsers)
            };

            await _unitOfWork.ConversationRepository.CreateNewConversationAsync(conversation);
            await _unitOfWork.CompleteTransactionAsync();

            int id = conversation.Id;

            return new ServiceResult<int>(true, "Conversation created successfully", id);
        }
    }
}
