using AutoMapper;
using GameChat.Core.DTOs;
using GameChat.Core.Helpers;
using GameChat.Core.Interfaces.Repositories;
using GameChat.Core.Interfaces.Services;
using GameChat.Core.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GameChat.Core.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConversationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task<ServiceResult<IEnumerable<ConversationDto>>> GetConversationsForUser(int userId)
        {
            var user = await _unitOfWork.UserRepository.FindByIdAsync(userId);

            if (user == null)
                return new ServiceResult<IEnumerable<ConversationDto>>(false, "Specified user does not exist");

            var conversations = await _unitOfWork.ConversationRepository.GetConversationsForUser(userId);
            var conversationsDto = _mapper.Map<IEnumerable<ConversationDto>>(conversations);

            foreach (var conversation in conversationsDto)
            {
                var participants = await _unitOfWork.ConversationRepository.GetParticipantsAsUsersAsync(conversation.Id);
                var participantsDto = _mapper.Map<IEnumerable<UserDto>>(participants).ToList();
                conversation.Participants = new Collection<UserDto>(participantsDto);
            }

            return new ServiceResult<IEnumerable<ConversationDto>>(true, "Conversations retrieved successfully", conversationsDto);
        }
    }
}
